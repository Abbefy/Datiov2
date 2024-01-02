using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Datiov2.Models;
using Datiov2.Data;
using System.Linq;


namespace Datiov2.Controllers
{
    public class ProductController : Controller
    {
        ProductMethods productMethods = new ProductMethods();


        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int id)
        {
            var product = productMethods.GetProductById(id);

            var random = new Random();
            var randomProducts = new List<ProductModel>();
            //for (int i = 0; i < 5; i++)
            //{
            //    randomProducts.Add(productMethods.GetProductById(random.Next(1, productMethods.GetNumberOfProducts())));

            //}
            //ViewBag.RandomProducts = randomProducts;



            var totalProducts = productMethods.GetNumberOfProducts();
            while (randomProducts.Count < 5)
            {
                var randomId = random.Next(1, totalProducts);
                if (randomId != id && !randomProducts.Any(p => p.ProductID == randomId))
                {
                    var randomProduct = productMethods.GetProductById(randomId);
                    if (randomProduct != null)
                    {
                        randomProducts.Add(randomProduct);
                    }
                }
            }
            ViewBag.RandomProducts = randomProducts;


            return View(product);
        }

        public IActionResult ShowRandomProducts(int amountOfRandomProducts)
        {
            List<ProductModel> products = productMethods.GetRandomProducts(amountOfRandomProducts);
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            List<string> imageUrls = new List<string>()
            {
                "/bilder/black_friday_pic",
                "/bilder/facebook",
            };
            ViewBag.ImageUrls = imageUrls;



            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel product)
        {
            productMethods.AddProduct(product);


            return RedirectToAction("Index", "Home");
        }

        public IActionResult Search(string search)
        {
            List<ProductModel> foundProducts = productMethods.SearchForProducts(search);
            ViewBag.FoundProducts = foundProducts;
            ViewBag.Search = search;
            return View(foundProducts);
            
        }

        public IActionResult AllProducts()
        {
            List<ProductModel> products = productMethods.GetAllProducts();
            ViewBag.AllProducts = products;
            return View(products);
        }



        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
