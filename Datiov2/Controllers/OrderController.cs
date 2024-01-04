using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Datiov2.Models;
using Datiov2.Data;


namespace Datiov2.Controllers
{
    public class OrderController : Controller
    {
        OrderMethods orderMethods = new OrderMethods();
        ProductMethods productMethods = new ProductMethods();
        CartMethods cartMethods = new CartMethods();
        OrderModel order = new OrderModel();
        List<CartItemModel> orderItems = new List<CartItemModel>();

        public IActionResult Order(int orderID)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userID = (int)HttpContext.Session.GetInt32("UserID");

            int cartID = cartMethods.GetCartID(userID);

            var cartItems = cartMethods.GetCartItems(cartID);

            ViewBag.CartItems = cartItems;

            return View();
        }

        //public int CreateOrder(int userID, int cartID, int orderPrice, string orderAddress, string orderFirstName, string orderLastName, int orderPostalCode, string orderCity)
        //{
        //    int orderID = 0;
        //    int rowsAffected = 0;

        //    SqlConnection dbConnection = new SqlConnection();
        //    dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
        //    string sqlString = "INSERT INTO dbo.Orders (OrderUserID, OrderPrice, OrderAddress, OrderFirstName, OrderLastName, OrderPostalCode, OrderCity) VALUES (@UserID, @OrderPrice, @OrderAddress, @OrderFirstName, @OrderLastName, @OrderPostalCode, @OrderCity); SELECT SCOPE_IDENTITY();";

        //    SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

        //    dbCommand.Parameters.AddWithValue("@UserID", userID);
        //    dbCommand.Parameters.AddWithValue("@OrderPrice", orderPrice);
        //    dbCommand.Parameters.AddWithValue("@OrderAddress", orderAddress);
        //    dbCommand.Parameters.AddWithValue("@OrderFirstName", orderFirstName);
        //    dbCommand.Parameters.AddWithValue("@OrderLastName", orderLastName);
        //    dbCommand.Parameters.AddWithValue("@OrderPostalCode", orderPostalCode);
        //    dbCommand.Parameters.AddWithValue("@OrderCity", orderCity);

        //    try
        //    {
        //        dbConnection.Open();
        //        // Use ExecuteScalar to get the last inserted identity value.
        //        orderID = Convert.ToInt32(dbCommand.ExecuteScalar());
        //        dbConnection.Close();
        //        return orderID;
        //    }
        //    catch (SqlException ex)
        //    {
        //        // Log exception
        //        // Display or return user-friendly error
        //    }
        //    return orderID;
        //}


        //        orderItems = cartMethods.GetCartItems(cartID);

        //            foreach (var item in orderItems)
        //            {
        //                orderMethods.AddToOrderItems(orderID, item.ProductID, item.Quantity, item.Price);
        //            }

        //    cartMethods.ClearCart(cartID);

        //            return RedirectToAction("OrderConfirmation", "Order", new { orderID = orderID
        //});


        [HttpPost]
        public IActionResult CreateOrder(int userID, int cartID, int orderPrice, string orderAddress, string orderFirstName, string orderLastName, int orderPostalCode, string orderCity)
        {
            int userIDForViewbag = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.UserID = userIDForViewbag;

            try
            {
                //int cartID = cartMethods.GetCartID(userID);
                //ViewBag.CartID = cartID;
                int orderID = orderMethods.CreateOrder(userID, cartID, orderPrice, orderAddress, orderFirstName, orderLastName, orderPostalCode, orderCity);

                if (orderID > 0)
                {
                    return RedirectToAction("Order", "Order", new { orderID = orderID });
                    //return RedirectToAction("Order", "Order", 
                }
                else
                {
                    return RedirectToAction("Cart", "Cart");
                }

            }
            catch
            {
                return View();
            }

        }



        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
