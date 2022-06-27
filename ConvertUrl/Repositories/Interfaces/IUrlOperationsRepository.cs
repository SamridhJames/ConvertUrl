using ConvertUrlRepository.Entities;

namespace ConvertUrlRepository.Repositories.Interfaces
{
    public interface IUrlOperationsRepository : IBasicRepository<UrlData>
    {
        UrlData GetShortUrl(string shortUrl);
        UrlData GetLongUrl(string longUrl);
    }
}
