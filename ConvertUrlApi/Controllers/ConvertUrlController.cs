using ConvertUrlService.Interfaces;
using ConvertUrlService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConvertUrlApi.Controllers
{
    [Route("")]
    [ApiController]
    public class ConvertUrlController : ControllerBase
    {
        private readonly IConvertUrlService _convertUrlService;

        public ConvertUrlController(IConvertUrlService convertUrlService)
        {
            _convertUrlService = convertUrlService;
        }

        [HttpGet]
        public IActionResult Get(string uc)
        {
            var longUrl = _convertUrlService.RetrieveLongUrl(uc);
            return !string.IsNullOrEmpty(longUrl) ? Redirect(longUrl) : NotFound();
        }

        [HttpPost]
        public IActionResult Post(ShortenUrl longUrl)
        {
            var uniqueCode = _convertUrlService.ConvertLongUrlToShortUrl(longUrl.Url);
            return !string.IsNullOrEmpty(uniqueCode) ? Ok(new ShortenUrl {Url = uniqueCode}) : NotFound();
        }
    }
}
