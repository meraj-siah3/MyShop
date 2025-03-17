using DataAccess.data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfwork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Add(obj);
                _unitOfwork.Save();
                TempData["success"] = "Category Created Successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryDb = _unitOfwork.Category.GetById(u => u.Id == id);
            if (CategoryDb == null)
            {
                return NotFound();
            }
            return View(CategoryDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Update(obj);
                _unitOfwork.Save();
                TempData["success"] = "Category Updated Successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryDb = _unitOfwork.Category.GetById(u => u.Id == id);
            if (CategoryDb == null)
            {
                return NotFound();
            }
            return View(CategoryDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? obj = _unitOfwork.Category.GetById(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfwork.Category.Remove(obj);
            _unitOfwork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");




        }
    }
}
