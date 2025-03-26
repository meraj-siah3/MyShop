using DataAccess.data;
using DataAccess.Repository.IRepository;
using DocumentFormat.OpenXml.Office2010.Excel;
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
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    //finaly name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //finaly masir
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        //delete old image
                        var OldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }

                    }
                        //uplode new image
                        using ( var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\Images\Product\" + fileName; 
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfwork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfwork.Product.Update(obj.Product);
                }
               
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
