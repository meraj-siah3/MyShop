using DataAccess.data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfwork.Product.GetAll().ToList();
       
            return View(objProductList);
        }

        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };
            return View(productVM);
        }
        

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Product.Add(obj);
                _unitOfwork.Save();
                TempData["success"] = "Product Created Successfully";

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
            Product? ProductDb = _unitOfwork.Product.GetById(u => u.Id == id);
            if (ProductDb == null)
            {
                return NotFound();
            }
            return View(ProductDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Product.Update(obj);
                _unitOfwork.Save();
                TempData["success"] = "Product Updated Successfully";

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
            Product? ProductDb = _unitOfwork.Product.GetById(u => u.Id == id);
            if (ProductDb == null)
            {
                return NotFound();
            }
            return View(ProductDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product? obj = _unitOfwork.Product.GetById(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfwork.Product.Remove(obj);
            _unitOfwork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");




        }
    }
}
