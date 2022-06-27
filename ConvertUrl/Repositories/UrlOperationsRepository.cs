using System;
using System.Linq;
using ConvertUrlRepository.Entities;
using ConvertUrlRepository.Repositories.Interfaces;

namespace ConvertUrlRepository.Repositories
{
    public class UrlOperationsRepository : BasicRepository<UrlData>, IUrlOperationsRepository
    {
        public UrlOperationsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public UrlData GetShortUrl(string shortUrl)
        {
            var urlData = Db.FirstOrDefault(x => x.ShortUrl == shortUrl && x.ExpiredDate > DateTime.UtcNow);

            if (urlData != null)
                SaveChange();

            return urlData;
        }

        public UrlData GetLongUrl(string longUrl)
        {
            var urlData = Db.FirstOrDefault(x => x.LongUrl == longUrl);

            return urlData;
        }
    }
}
