using Datiov2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;



namespace Datiov2.Controllers
{
    public class UserController : Controller
    {
        UserMethods userMethods = new UserMethods();





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

        //[HttpPost]
        //public IActionResult Wishlist(



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








        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
