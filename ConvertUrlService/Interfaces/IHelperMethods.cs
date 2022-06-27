namespace ConvertUrlService.Interfaces
{
    public interface IHelperMethods
    {
        bool IsLongUrlValid(string longUrl);
        bool IsShortUrlValid(string shortUrl);
        string GenerateShortUrl(int length = 6);
        string GetLongUrlFromCache(string shortUrl);
        void AddLongUrlIntoCache(string longUrl, string shortUrl);
        int GetExpirationDate();
        string GetBaseAddress();
    }
}
