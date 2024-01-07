using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Datiov2.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

//CREATE TABLE[dbo].[Categories] (
//    [CategoryID]   INT IDENTITY(1, 1) NOT NULL,
//    [CategoryName] NVARCHAR (100) NOT NULL,
//    [CategoryMainpage]     BIT            NOT NULL DEFAULT ((0)),
//    PRIMARY KEY CLUSTERED ([CategoryID] ASC)

//);



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
                    int categoryRank = (int)categoryReader["CategoryRank"];


                    CategoryModel category = new CategoryModel
                    {
                        CategoryID = categoryId,
                        CategoryName = categoryName,
                        CategoryRank = categoryRank,
                        Products = GetProductsByCategory(categoryId)
                    };
                    categories.Add(category);
                }
                dbConnection.Close();
            }
            return categories;
        }

        public List<CategoryModel> GetAllCategories()
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT * FROM Categories";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbConnection.Open();

            SqlDataReader dbReader = dbCommand.ExecuteReader();

            List<CategoryModel> categories = new List<CategoryModel>();

            while (dbReader.Read())
            {
                int categoryId = (int)dbReader["CategoryID"];
                string categoryName = dbReader["CategoryName"].ToString();
                int categoryRank = (int)dbReader["CategoryRank"];

                CategoryModel category = new CategoryModel
                {
                    CategoryID = categoryId,
                    CategoryName = categoryName,
                    CategoryRank = categoryRank
                };
                categories.Add(category);
            }
            dbConnection.Close();
            return categories;
            }





        private List<ProductModel> GetProductsByCategory(int categoryID)
        {
            ProductMethods productMethods = new ProductMethods();
            return productMethods.GetProductsByCategory(categoryID);
        }

        public void CreateCategory(string categoryName, int categoryRank)
        {
            using (SqlConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                const string categoryQuery = "INSERT INTO Categories (CategoryName, CategoryRank) VALUES (@CategoryName, @CategoryRank)";
                SqlCommand categoryCommand = new SqlCommand(categoryQuery, dbConnection);
                categoryCommand.Parameters.AddWithValue("@CategoryName", categoryName);
                categoryCommand.Parameters.AddWithValue("@CategoryRank", categoryRank);

                categoryCommand.ExecuteNonQuery();

                dbConnection.Close();
            }
        }

        public void DeleteCategory(int categoryID)
        {
            using (SqlConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                const string categoryQuery = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                SqlCommand categoryCommand = new SqlCommand(categoryQuery, dbConnection);
                categoryCommand.Parameters.AddWithValue("@CategoryID", categoryID);

                categoryCommand.ExecuteNonQuery();

                dbConnection.Close();
            }
        }

        public void UpdateCategory(CategoryModel category)
        {
            using (SqlConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                const string categoryQuery = "UPDATE Categories SET CategoryName = @CategoryName, CategoryRank = @CategoryRank WHERE CategoryID = @CategoryID";
                SqlCommand categoryCommand = new SqlCommand(categoryQuery, dbConnection);
                categoryCommand.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                categoryCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                categoryCommand.Parameters.AddWithValue("@CategoryRank", category.CategoryRank);

                categoryCommand.ExecuteNonQuery();

                dbConnection.Close();
            }
        }

        public CategoryModel GetCategory(int categoryID)
        {
            CategoryModel category = new CategoryModel();

            using (SqlConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                const string categoryQuery = "SELECT * FROM Categories WHERE CategoryID = @CategoryID";
                SqlCommand categoryCommand = new SqlCommand(categoryQuery, dbConnection);
                categoryCommand.Parameters.AddWithValue("@CategoryID", categoryID);
                SqlDataReader categoryReader = categoryCommand.ExecuteReader();

                while (categoryReader.Read())
                {
                    int categoryId = (int)categoryReader["CategoryID"];
                    string categoryName = categoryReader["CategoryName"].ToString();
                    int categoryRank = (int)categoryReader["CategoryRank"];

                    category.CategoryID = categoryId;
                    category.CategoryName = categoryName;
                    category.CategoryRank = categoryRank;
                }
                dbConnection.Close();
            }
            return category;
        }








    }
}