using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Datiov2.Models;
using Datiov2.Data;



namespace Datiov2.Controllers
{
    public class CartController : Controller
    {
        CartMethods cartMethods = new CartMethods();
        ProductMethods productMethods = new ProductMethods();
        CartModel cart = new CartModel();
        List <CartItemModel> cartItems = new List<CartItemModel>();

        

        // GET: CartController
        public IActionResult Cart()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userID = (int)HttpContext.Session.GetInt32("UserID");
            //if the user has no cart, create one

            int cartID = cartMethods.GetCartID(userID);

            var cartItems = cartMethods.GetCartItems(cartID);
            
            ViewBag.CartItems = cartItems;

            
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productID, int quantity, int price)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userID = (int)HttpContext.Session.GetInt32("UserID");

            int cartID = cartMethods.GetCartID(userID);

            cartMethods.AddToCart(cartID, productID, quantity, price);

            //if (error != "")
            //{
            //    ViewBag.Error = error;
            //    return View();
            //}

            return RedirectToAction("Cart", "Cart");
        }



    }
}
