using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Datiov2.Models;
using Datiov2.Data;
using datiov2.models;



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
            int cartID = cartMethods.GetCartID(userID);

            cartItems = cartMethods.GetCartItems(cartID);


            return View(cartItems);
        }



    }
}
