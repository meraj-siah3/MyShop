using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using MyShop.Models;
using System.Diagnostics;

namespace MyShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfwork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfwork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitOfwork.Product.GetAll(IncludeProperties: "Category");

            return View(ProductList);
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
