using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public ContentResult Index()
        {
            var filePath = Path.Combine(_env.WebRootPath, "index.cshtml");
            var html = System.IO.File.ReadAllText(filePath);
            return Content(html, "text/html");
        }
    }
}
