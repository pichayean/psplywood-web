using Microsoft.AspNetCore.Mvc;
using PSPlywoodWeb.Models.Article;
using PSPlywoodWeb.Services;

namespace PSPlywoodWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IPSPlywoodService _psPlywoodService;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IPSPlywoodService psPlywoodService, ILogger<ArticleController> logger)
        {
            _psPlywoodService = psPlywoodService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {

            var articles = await _psPlywoodService.GetArticlesAsync();
            var a = new ArticleListViewModel();
            a.Articles = articles;
            return View(a);
        }
        public async Task<IActionResult> ArticleDetail(int id)
        {

            var article = await _psPlywoodService.GetArticleAsync(id);
            var a = new ArticleViewModel();
            a.Article = article;
            return View(a);
        }

        
    }
}
