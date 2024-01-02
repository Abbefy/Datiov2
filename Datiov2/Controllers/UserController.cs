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



namespace Datiov2.Controllers
{
    public class UserController : Controller
    {
        UserMethods userMethods = new UserMethods();
        WishlistMethods wishlistMethods = new WishlistMethods();





        // GET: UserController
        public IActionResult Index()
        {
            return View();
        }

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

           // HttpContext.Session.SetInt32("IsLoggedIn", 1);
            HttpContext.Session.SetInt32("UserID", userLogin.UserID);
            HttpContext.Session.SetString("UserName", userLogin.UserName);
            HttpContext.Session.SetInt32("UserType", userLogin.UserType);

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



            if (insert == 0)
            {
                ViewBag.Error = error;
                throw new Exception(error);
                //return View();
            }

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
