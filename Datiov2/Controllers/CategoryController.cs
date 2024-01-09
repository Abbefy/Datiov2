using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Datiov2.Models;
using Datiov2.Data;
using System.Linq;
using Newtonsoft.Json;


namespace Datiov2.Controllers
{
    public class CategoryController : BaseController
    {
        ProductMethods productMethods = new ProductMethods();
        WishlistMethods wishlistMethods = new WishlistMethods();
        CategoryMethods categoryMethods = new CategoryMethods();


     

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryModel category)
        {
            categoryMethods.CreateCategory(category.CategoryName, category.CategoryRank);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteCategory(int categoryID)
        {
            CategoryModel category = categoryMethods.GetCategory(categoryID);
            ViewBag.Category = category;
            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteCategory(CategoryModel category)
        {
            categoryMethods.DeleteCategory(category);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateCategory(int categoryID)
        {
            CategoryModel category = categoryMethods.GetCategory(categoryID);
            ViewBag.Category = category;
            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryModel category)
        {
            categoryMethods.UpdateCategory(category);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Category(int categoryID)
        {
            List<ProductModel> products = productMethods.GetProductsByCategory(categoryID);
            ViewBag.ProductsWithCategory = products;
            return View(products);
        }



    }
}
