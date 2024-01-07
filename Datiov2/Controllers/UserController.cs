using Datiov2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Datiov2.Data;
using Microsoft.EntityFrameworkCore;
using Datiov2.Data;



namespace Datiov2.Controllers
{
    public class UserController : BaseController
    {
        UserMethods userMethods = new UserMethods();
        WishlistMethods wishlistMethods = new WishlistMethods();
        CartMethods CartMethods = new CartMethods();
        OrderMethods orderMethods = new OrderMethods();






        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                UserModel loggedInUser = userMethods.GetAccount((int)HttpContext.Session.GetInt32("UserID"), out string error);

                return RedirectToAction("Account", "User", loggedInUser);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {




            UserModel userLogin = new UserModel();
            string error = "";

            userLogin = userMethods.Login(user.UserName, user.UserPass, out error);

            if (userLogin == null)
            {
                ViewBag.Error = error;
                return View();
            }

            int cartID = CartMethods.GetCartID(userLogin.UserID);

           // HttpContext.Session.SetInt32("IsLoggedIn", 1);
            HttpContext.Session.SetInt32("UserID", userLogin.UserID);
            HttpContext.Session.SetString("UserName", userLogin.UserName);
            HttpContext.Session.SetInt32("UserType", userLogin.UserType);

            HttpContext.Session.SetInt32("CartItemCount", CartMethods.GetCartItemCount(cartID));


            return RedirectToAction("Account", "User", userLogin);

        }

        [HttpGet]
        public IActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            int insert = 0;
            string error = "";

            Console.WriteLine("HELLOOOOOOOOOOOOOOOOO");

            insert = userMethods.Register(user, out error);

            int userID = userMethods.GetUserID(user.UserName, out error);



            if (insert == 0)
            {
                ViewBag.Error = error;
                throw new Exception(error);
                //return View();
            }

            CartMethods.CreateCart(userID);
            return RedirectToAction("Index", "Home");



        }

        [HttpGet]
        public IActionResult Account(UserModel user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "User");
            } 

            UserModel userAccount = new UserModel();
            string error = "";

            userAccount = userMethods.GetAccount(user.UserID, out error);

            if (userAccount == null)
            {
                ViewBag.Error = error;
                return View();
            }

            List<OrderModel> orders = orderMethods.GetOrders(user.UserID);
            ViewBag.Orders = orders;

            return View(userAccount);
        }

        
        public IActionResult Wishlist()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userID = (int)HttpContext.Session.GetInt32("UserID");
            var products = wishlistMethods.GetWishListProducts(userID);
            //List<WishlistModel> wishlist = wishlistMethods.ViewWishlist(userID);
            ViewBag.WishlistProducts = products;

            //ViewBag.Wishlist = wishlist;

            return View(products);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteAccount(int userID)
        {
            HttpContext.Session.Clear();
            int delete = userMethods.DeleteAccount(userID, out string error);

            if (delete == 0)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }


        //public IActionResult DeleteAccount(int userID)
        //{
        //    string error = string.Empty;
        //    int rowsAffected = 0;

        //    try
        //    {
        //        // Begin a database transaction
        //        using (var dbContextTransaction = dbContext.Database.BeginTransaction())
        //        {
        //            // Delete the user from the Users table
        //            rowsAffected = userMethods.DeleteAccount(userID, out error);

        //            if (rowsAffected > 0)
        //            {
        //                // Find and update the related orders
        //                var ordersToUpdate = dbContext.Orders.Where(o => o.OrderUserID == userID).ToList();

        //                foreach (var order in ordersToUpdate)
        //                {
        //                    order.OrderUserID = null; // Set to NULL or another default value
        //                }

        //                // Save changes to the database
        //                dbContext.SaveChanges();

        //                // Commit the transaction
        //                dbContextTransaction.Commit();
        //            }
        //            else
        //            {
        //                // If the user deletion failed, roll back the transaction
        //                dbContextTransaction.Rollback();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.Message;
        //    }

        //    if (!string.IsNullOrEmpty(error))
        //    {
        //        ViewBag.Error = error;
        //        return View();
        //    }

        //    return RedirectToAction("Index", "Home");
        //}









        [HttpGet]
        public IActionResult UpdateAccount(int userID)
        {
            UserModel user = userMethods.GetAccount(userID, out string error);

            return View(user);
        }
        public IActionResult UpdateAccount(UserModel user)
        {
            int update = userMethods.UpdateAccount(user, out string error);

            if (update == 0)
            {
                ViewBag.Error = error;
                return View();
            }


            return RedirectToAction("Account", "User", user);
        }



        //[HttpPost]
        //public IActionResult Account(UserModel user)
        //{
        //    if (HttpContext.Session.GetString("UserName") == null)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //    UserModel userAccount = new UserModel();
        //    string error = "";

        //    userAccount = userMethods.GetAccount(user.UserID, out error);

        //    if (userAccount == null)
        //    {
        //        ViewBag.Error = error;
        //        return View();
        //    }

        //    return View(userAccount);
        //}



//namespace Datiov2.Models
//    {
//        public class WishlistMethods
//        {
//            public List<WishlistModel> ViewWishlist(int userID)
//            {
//                List<WishlistModel> wishlist = new List<WishlistModel>();

//                SqlConnection dbConnection = new SqlConnection();
//                dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//                string sqlString = "SELECT * FROM dbo.Wishlist WHERE UserID = @UserID";

//                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//                dbCommand.Parameters.AddWithValue("@UserID", userID);

//                try
//                {
//                    dbConnection.Open();
//                    SqlDataReader dbReader = dbCommand.ExecuteReader();

//                    while (dbReader.Read())
//                    {
//                        WishlistModel wishlistItem = new WishlistModel();

//                        wishlistItem.WishlistID = (int)dbReader["WishlistID"];
//                        wishlistItem.WishlistProductID = (int)dbReader["ProductID"];
//                        wishlistItem.WishlistUserID = (int)dbReader["UserID"];

//                        wishlist.Add(wishlistItem);
//                    }
//                    dbConnection.Close();
//                    return wishlist;
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }


//            }

//            public int AddToWishlist(int userID, int productID)
//            {
//                SqlConnection dbConnection = new SqlConnection();
//                dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//                string sqlString = "INSERT INTO dbo.Wishlist (UserID, ProductID) VALUES (@UserID, @ProductID)";

//                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//                dbCommand.Parameters.AddWithValue("@UserID", userID);
//                dbCommand.Parameters.AddWithValue("@ProductID", productID);

//                try
//                {
//                    dbConnection.Open();
//                    int rowsAffected = dbCommand.ExecuteNonQuery();
//                    dbConnection.Close();
//                    return rowsAffected;
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }

//            public int RemoveProductFromWishlist(int UserID, int ProductID)
//            {
//                SqlConnection dbConnection = new SqlConnection();
//                dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//                string sqlString = "DELETE FROM dbo.Wishlist WHERE UserID = @UserID AND ProductID = @ProductID";

//                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//                dbCommand.Parameters.AddWithValue("@UserID", UserID);
//                dbCommand.Parameters.AddWithValue("@ProductID", ProductID);

//                try
//                {
//                    dbConnection.Open();
//                    int rowsAffected = dbCommand.ExecuteNonQuery();
//                    dbConnection.Close();
//                    return rowsAffected;
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }
//            }




//        }

//    }



}
}
