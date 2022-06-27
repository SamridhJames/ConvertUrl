using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ConvertUrlRepository.Domain;
using ConvertUrlService.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ConvertUrlService
{
    public class HelperMethods : IHelperMethods
    {
        private const string BaseAddress = "https://localhost:44382/?uc=";

        private readonly int _expirationTimeInDays;
        private readonly string _urlValidationRegex;
        private readonly int _shortUrlMinLength;
        private readonly int _shortUrlMaxLength;
        private readonly int _characterBase;
        private static char[] _shortCodeCharacterSetArray;
        private static RNGCryptoServiceProvider _cryptoServiceProvider;
        private readonly IMemoryCache _cache;

        public HelperMethods(IOptions<AppSettings> urlSettings, IMemoryCache cache)
        {
            _expirationTimeInDays = urlSettings.Value.ExpirationTimeInDays;
            _urlValidationRegex = urlSettings.Value.LongUrlValidation;
            _shortUrlMinLength = urlSettings.Value.ShortUrlMinLength;
            _shortUrlMaxLength = urlSettings.Value.ShortUrlMaxLength;
            _shortCodeCharacterSetArray = urlSettings.Value.ShortCodeBaseSet.ToCharArray();
            _characterBase = _shortCodeCharacterSetArray.Length;
            _cryptoServiceProvider = new RNGCryptoServiceProvider();
            _cache = cache;
        }

        public bool IsLongUrlValid(string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl)) return false;
            var regexPattern = new Regex(_urlValidationRegex, RegexOptions.IgnoreCase);
            return regexPattern.IsMatch(longUrl);
        }

        public bool IsShortUrlValid(string shortUrl)
        {
            var pattern = $"^[a-zA-Z0-9]{{{_shortUrlMinLength},{_shortUrlMaxLength}}}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(shortUrl);
        }

        public string GenerateShortUrl(int length = 6)
        {
            var byteArray = new byte[length];
            _cryptoServiceProvider.GetNonZeroBytes(byteArray);

            var shortCode = new StringBuilder(length);
            foreach (var eachByte in byteArray)
            {
                var position = eachByte % (_characterBase - 1);
                shortCode.Append(_shortCodeCharacterSetArray[position]);
            }

            return shortCode.ToString();
        }

        public string GetLongUrlFromCache(string shortUrl)
        {
            return _cache.TryGetValue(shortUrl, out string longUrl) ? longUrl : string.Empty;
        }

        public void AddLongUrlIntoCache(string longUrl, string shortUrl)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
            _cache.Set(shortUrl, longUrl, cacheEntryOptions);
        }

        public int GetExpirationDate()
        {
            return _expirationTimeInDays;
        }

        public string GetBaseAddress()
        {
            return BaseAddress;
        }
    }
}
