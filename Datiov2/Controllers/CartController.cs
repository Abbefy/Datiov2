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

            ViewBag.CartID = cartID;

            ViewBag.UserID = userID;

            
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

            int existingCartItemID = cartMethods.GetCartItemID(cartID, productID);

            //if the product is already in the cart, update the quantity
            if (existingCartItemID > 0)
            {
                //int cartItemID = cartMethods.GetCartItemID(cartID, productID);
                cartMethods.UpdateCartItemQuantity(existingCartItemID, quantity);
            } else
            {
                cartMethods.AddToCart(cartID, productID, quantity, price);
            }

            //<span class="cart-item-count">@ContextAccessor.HttpContext.Session.GetInt32("CartItemCount")</span>
            HttpContext.Session.SetInt32("CartItemCount", cartMethods.GetCartItemCount(cartID));





            //if (error != "")
            //{
            //    ViewBag.Error = error;
            //    return View();
            //}

            return RedirectToAction("Cart", "Cart");
        }

        public IActionResult RemoveFromCart(int cartItemID)
        {

            int userID = (int)HttpContext.Session.GetInt32("UserID");

            int cartID = cartMethods.GetCartID(userID);

            cartMethods.DeleteCartItem(cartItemID);

            return RedirectToAction("Cart", "Cart");
        }

        public IActionResult UpdateQuantity(int cartItemID, int quantity)
        {
            cartMethods.UpdateCartItemQuantityCheckout(cartItemID, quantity);

            return RedirectToAction("Cart", "Cart");
        }



    }
}
