using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Datiov2.Models; // Ensure this is the correct namespace for ProductModel
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Data.SqlClient;




namespace Datiov2.Data
{
    public class ProductMethods
    {
        public List<ProductModel> GetAllProducts()
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "SELECT * FROM Products";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbConnection.Open();
            SqlDataReader dbReader = dbCommand.ExecuteReader();

            List<ProductModel> products = new List<ProductModel>();
            while (dbReader.Read())
            {
                   ProductModel product = new ProductModel
                   {
                    ProductID = (int)dbReader["ProductID"],
                    ProductName = dbReader["ProductName"].ToString(),
                    ProductImage = dbReader["ProductImage"].ToString(),
                    ProductDescription = dbReader["ProductDescription"].ToString(),
                    ProductPrice = (int)dbReader["ProductPrice"],
                    ProductStock = (int)dbReader["ProductStock"],
                    ProductCategoryID = (int)dbReader["ProductCategoryID"]
                };
                products.Add(product);
            }
            dbConnection.Close();
            return products;


            //List<ProductModel> products = new List<ProductModel>();

            //using (SqlConnection dbConnection = new SqlConnection(connectionString))
            //{
            //    const string query = "SELECT * FROM Products";
            //    SqlCommand command = new SqlCommand(query, dbConnection);

            //    dbConnection.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        ProductModel product = new ProductModel
            //        {
            //            ProductID = (int)reader["ProductID"],
            //            ProductName = reader["ProductName"].ToString(),
            //            ProductCategory = reader["ProductCategory"].ToString(),
            //            ProductImage = reader["ProductImage"].ToString(),
            //            ProductDescription = reader["ProductDescription"].ToString(),
            //            ProductPrice = (int)reader["ProductPrice"],
            //            ProductStock = (int)reader["ProductStock"],
            //            ProductCategoryID = (int)reader["ProductCategoryID"]
            //        };
            //        products.Add(product);
            //    }
            //    dbConnection.Close();
            //}

            //return products;
        }

        public List<ProductModel> GetProductsByCategory(int categoryID)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "SELECT * FROM Products WHERE ProductCategoryID = @categoryID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbCommand.Parameters.AddWithValue("@categoryID", categoryID);
            dbConnection.Open();
            SqlDataReader dbReader = dbCommand.ExecuteReader();

            List<ProductModel> products = new List<ProductModel>();
            while (dbReader.Read())
            {
                ProductModel product = new ProductModel
                {
                    ProductID = (int)dbReader["ProductID"],
                    ProductName = dbReader["ProductName"].ToString(),
                    ProductImage = dbReader["ProductImage"].ToString(),
                    ProductDescription = dbReader["ProductDescription"].ToString(),
                    ProductPrice = (int)dbReader["ProductPrice"],
                    ProductStock = (int)dbReader["ProductStock"],
                    ProductCategoryID = (int)dbReader["ProductCategoryID"]
                };
                products.Add(product);
            }
            dbConnection.Close();
            return products;
        }

        public ProductModel GetProductById(int productID)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "SELECT * FROM Products WHERE ProductID = @productID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbCommand.Parameters.AddWithValue("@productID", productID);
            dbConnection.Open();
            SqlDataReader dbReader = dbCommand.ExecuteReader();

            ProductModel product = new ProductModel();
            while (dbReader.Read())
            {
                product.ProductID = (int)dbReader["ProductID"];
                product.ProductName = dbReader["ProductName"].ToString();
                product.ProductImage = dbReader["ProductImage"].ToString();
                product.ProductDescription = dbReader["ProductDescription"].ToString();
                product.ProductPrice = (int)dbReader["ProductPrice"];
                product.ProductStock = (int)dbReader["ProductStock"];
                product.ProductCategoryID = (int)dbReader["ProductCategoryID"];
            }
            dbConnection.Close();
            return product;
        }




    }
}
