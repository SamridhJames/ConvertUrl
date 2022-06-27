
namespace ConvertUrlService.Interfaces
{
    public interface IConvertUrlService
    {
        string ConvertLongUrlToShortUrl(string longUrl);
        string RetrieveLongUrl(string shortUrl);
    }
}
