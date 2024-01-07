using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Datiov2.Models;
using Datiov2.Data;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Datiov2.Controllers
{
    public class BaseController : Controller
    {
        CategoryMethods categoryMethods = new CategoryMethods();

        public BaseController()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            CategoryMethods categoryMethods = new CategoryMethods();
            var categories = categoryMethods.GetAllCategoriesWithProducts();
            ViewBag.Categories = categories;

            base.OnActionExecuting(context);
        }



    }
}
