using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Datiov2.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Datiov2.Data
{
    public class CategoryMethods
    {
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Abbesdb; Integrated Security=True";

        public List<CategoryModel> GetAllCategoriesWithProducts()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using (SqlConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                const string categoryQuery = "SELECT * FROM Categories";
                SqlCommand categoryCommand = new SqlCommand(categoryQuery, dbConnection);
                SqlDataReader categoryReader = categoryCommand.ExecuteReader();

                while (categoryReader.Read())
                {
                    int categoryId = (int)categoryReader["CategoryID"];
                    string categoryName = categoryReader["CategoryName"].ToString();

                    CategoryModel category = new CategoryModel
                    {
                        CategoryID = categoryId,
                        CategoryName = categoryName,
                        Products = GetProductsByCategory(categoryId)
                    };
                    categories.Add(category);
                }
                dbConnection.Close();
            }
            return categories;
        }

        private List<ProductModel> GetProductsByCategory(int categoryID)
        {
            ProductMethods productMethods = new ProductMethods();
            return productMethods.GetProductsByCategory(categoryID);
        }
    }
}