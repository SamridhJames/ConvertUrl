
namespace ConvertUrlRepository.Domain
{
    public class AppSettings
    {
        public int ShortUrlMaxLength { get; set; }
        public int ShortUrlMinLength { get; set; }
        public int ExpirationTimeInDays { get; set; }
        public string ShortCodeBaseSet { get; set; }
        public string LongUrlValidation { get; set; }
    }
}
