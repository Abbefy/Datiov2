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
    public class ProductController : BaseController
    {
        ProductMethods productMethods = new ProductMethods();
        WishlistMethods wishlistMethods = new WishlistMethods();
        CategoryMethods categoryMethods = new CategoryMethods();


        List<string> imageUrls = new List<string>()
            {
                "~/bilder/black_friday_pic.jpg",
                "~/bilder/facebook",
                "/bilder/ps5_product_image.jpg",
                "/bilder/AMD_R7.webp",
            };


        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(int id)
        {
            var product = productMethods.GetProductById(id);

            try
            {
                var specifications = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(product.ProductSpecifications);
                ViewBag.ProductSpecifications = specifications;

            }
            catch (Exception)
            {
                ViewBag.ProductSpecifications = null;
            }

            //var specifications2 = new Dictionary<string, Dictionary<string, string>>();

            


    //        '{
    //"Processor": {
    //            "Antal kärnor": "6 kärnor",
    //    "Antal trådar": "12 trådar",
    //    "Cache": "38 MB",
    //    "Cacheinformation": "L3 - 32 MB, L2 - 6 MB",
    //    "Kvantitet": "1",
    //    "Klockfrekvens": "3.8 GHz",
    //    "Max. turbohastighet": "5.1 GHz",
    //    "Kompatibel processorsockel": "AM5",
    //    "Tillverkninsprocess": "5 nm",
    //    "Termisk avledningseffekt": "65 W",
    //    "PCI-expressrevision": "5.0",
    //    "Överclockningsstöd": "Ja",
    //    "Integrerad grafik": "Ja"
    //},
    //"Diverse": {
    //            "Inkluderade tillbehör": "AMD Wraith Stealth Cooler", 
    //    "Förpackningstyp": "Processor in a Box (PIB)"
    //},
    //"Allmänt": {
    //            "Produkttyp": "Processor",
    //    "Tillverkare": "AMD",
    //    "Producentens garanti (månader)": "36"
    //}
    //    }'

            //do something with specifications2






            //ViewBag.ProductSpecifications2 = specifications2;

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

            ViewBag.ImageUrls = imageUrls;

            List<CategoryModel> categories = categoryMethods.GetAllCategories();
            ViewBag.Categories = categories;




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


        public IActionResult AddToWishlist(int wishlistProductID)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }
            
            int productID = wishlistProductID;

            int userID = (int)HttpContext.Session.GetInt32("UserID");

            wishlistMethods.AddToWishlist(userID, productID, out string error);

            //if (error != "")
            //{
            //    ViewBag.Error = error;
            //    return View();
            //}
            
            return RedirectToAction("Index", "Home");

            
        }

        public IActionResult RemoveFromWishlist(int wishlistProductID)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }

            int productID = wishlistProductID;

            int userID = (int)HttpContext.Session.GetInt32("UserID");

            wishlistMethods.RemoveProductFromWishlist(userID, productID);
            return RedirectToAction("Wishlist", "User", userID);
        }

        public IActionResult DeleteProduct(int productID)
        {
            int deletedProductID = productMethods.DeleteProduct(productID);
            if (deletedProductID == 0)
            {
                ViewBag.Error = "Produkten kunde inte tas bort";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int productID)
        {

            ProductModel product = productMethods.GetProductById(productID);
            ViewBag.ImageUrls = imageUrls;
            ViewBag.Product = product;

            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductModel product)
        {
            productMethods.UpdateProduct(product);

            return RedirectToAction("Product", "Product", product.ProductID);
        }

        //[HttpGet]
        //public IActionResult AddCategory()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddCategory(CategoryModel category)
        //{
        //    categoryMethods.CreateCategory(category.CategoryName , category.CategoryRank);
        //    return RedirectToAction("Index", "Home");
        //}

        //public IActionResult DeleteCategory(int categoryID)
        //{
        //    categoryMethods.DeleteCategory(categoryID);
        //    return RedirectToAction("Index", "Home");
        //}

        //public IActionResult UpdateCategory(int categoryID)
        //{
        //    CategoryModel category = categoryMethods.GetCategory(categoryID);
        //    ViewBag.Category = category;
        //    return View(category);
        //}

        //[HttpPost]
        //public IActionResult UpdateCategory(CategoryModel category)
        //{
        //    categoryMethods.UpdateCategory(category);
        //    return RedirectToAction("Index", "Home");
        //}

        public IActionResult Category(int categoryID)
        {
            List<ProductModel> products = productMethods.GetProductsByCategory(categoryID);
            ViewBag.ProductsWithCategory = products;
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
