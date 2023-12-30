using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Datiov2.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public int ProductCategoryID { get; set; }



    }
}


