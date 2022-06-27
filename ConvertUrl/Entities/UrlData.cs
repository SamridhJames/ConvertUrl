
using ConvertUrlRepository.Domain;

namespace ConvertUrlRepository.Entities
{
    public class UrlData: BaseUrlEntity
    {
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
    }
}
