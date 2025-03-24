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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfwork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfwork.Product.GetAll().ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
              
                CategoryList = _unitOfwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if(id==0 || id == null)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfwork.Product.GetById(u => u.Id == id);
                return View(productVM);
            }
           
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPhath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    //finaly name
                    string fileName = Guid.NewGuid().ToString() +Path.GetExtension(file.Name);
                    //finaly masir
                    string productPhath = Path.Combine(wwwRootPhath, @"Images\Product");
                }
              
                _unitOfwork.Product.Add(obj.Product);
                _unitOfwork.Save();
                TempData["success"] = "Product Created Successfully";

                return RedirectToAction("Index");
            }
            else
                obj.CategoryList = _unitOfwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            return View(obj);
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
