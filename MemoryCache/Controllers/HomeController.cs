using MemoryCache.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemoryCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheManager _cacheManager;
        public HomeController(ILogger<HomeController> logger, ICacheManager cacheManager)
        {
            _logger = logger;
            _cacheManager = cacheManager;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _cacheManager.GetAsync("CodeCell.ir", async () => GetCodeCellCourseTitle(),10);
            return View(models);
        }

        private IEnumerable<string> GetCodeCellCourseTitle()
        {
            return new List<string>
            {
                "asp core 6",
                ".net maui",
                "cache",
                "dapper in asp core"
            };
        }
    }
}