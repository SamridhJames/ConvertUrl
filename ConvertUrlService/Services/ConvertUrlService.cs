using System;
using System.Net;
using ConvertUrlRepository.Entities;
using ConvertUrlRepository.Repositories.Interfaces;
using ConvertUrlService.Interfaces;

namespace ConvertUrlService.Services
{
    public class ConvertUrlService : IConvertUrlService
    {
        private readonly IUrlOperationsRepository _urlOperationsRepository;
        private readonly IHelperMethods _helperMethods;

        public ConvertUrlService(IUrlOperationsRepository urlOperationsRepository, IHelperMethods helperMethods)
        {
            _urlOperationsRepository = urlOperationsRepository;
            _helperMethods = helperMethods;
        }

        public string ConvertLongUrlToShortUrl(string longUrl)
        {
            if (!_helperMethods.IsLongUrlValid(longUrl)) return string.Empty;

            var encodedUrl = WebUtility.UrlEncode(longUrl);
            var existingUrlData = _urlOperationsRepository.GetLongUrl(encodedUrl);
            if (existingUrlData != null)
            {
                return $"{_helperMethods.GetBaseAddress()}{existingUrlData.ShortUrl}";
            }

            var uniqueCode = _helperMethods.GenerateShortUrl();
            if (string.IsNullOrEmpty(uniqueCode))
                return string.Empty;

            _urlOperationsRepository.Insert(CreateUrlDataObject(longUrl, uniqueCode));

            if (!_urlOperationsRepository.SaveChange()) return string.Empty;

            var shortUrl = $"{_helperMethods.GetBaseAddress()}{uniqueCode}";
            return shortUrl;
        }

        public string RetrieveLongUrl(string shortUrl)
        {
            if (!_helperMethods.IsShortUrlValid(shortUrl)) return string.Empty;

            var longUrl = _helperMethods.GetLongUrlFromCache(shortUrl);
            if (!string.IsNullOrEmpty(longUrl))
            {
                return longUrl;
            }

            var urlData = _urlOperationsRepository.GetShortUrl(shortUrl);
            if (urlData == null) return string.Empty;

            if (!urlData.LongUrl.StartsWith(Uri.UriSchemeHttps))
            {
                urlData.LongUrl = Uri.UriSchemeHttps + "://" + urlData.LongUrl;
            }
            _helperMethods.AddLongUrlIntoCache(urlData.LongUrl, shortUrl);
            return urlData.LongUrl;
        }

        private UrlData CreateUrlDataObject(string longUrl, string shortUrl)
        {
            return new()
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                CreatedDate = DateTime.UtcNow,
                ExpiredDate = DateTime.UtcNow.AddDays(_helperMethods.GetExpirationDate()),
            };
        }
    }
}
