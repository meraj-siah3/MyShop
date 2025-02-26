using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
