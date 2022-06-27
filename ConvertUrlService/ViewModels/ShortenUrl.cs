using System.ComponentModel.DataAnnotations;

namespace ConvertUrlService.ViewModels
{
    public class ShortenUrl
    {
        [Required]
        public string Url { get; set; }
    }
}
