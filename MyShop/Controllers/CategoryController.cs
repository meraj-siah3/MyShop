using DataAccess.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
       
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
