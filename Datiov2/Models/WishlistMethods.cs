using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Datiov2.Models;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;



namespace Datiov2.Models
{
    public class WishlistMethods
    {
        public List<WishlistModel> ViewWishlist(int userID)
        {
            List<WishlistModel> wishlist = new List<WishlistModel>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT * FROM dbo.Wishlist WHERE WishlistUserID = @UserID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", userID);

            try
            {
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();

                while (dbReader.Read())
                {
                    WishlistModel wishlistItem = new WishlistModel();

                    wishlistItem.WishlistID = (int)dbReader["WishlistID"];
                    wishlistItem.WishlistProductID = (int)dbReader["WishlistProductID"];
                    wishlistItem.WishlistUserID = (int)dbReader["WishlistUserID"];

                    wishlist.Add(wishlistItem);
                }
                dbConnection.Close();
                return wishlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }


        }

        public int AddToWishlist(int userID, int productID, out string error)
        {
            error = "";
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "INSERT INTO dbo.Wishlist (WishlistUserID, WishlistProductID) VALUES (@UserID, @ProductID)";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", userID);
            dbCommand.Parameters.AddWithValue("@ProductID", productID);

            try
            {
                dbConnection.Open();
                int wishlistAffected = dbCommand.ExecuteNonQuery();

                return wishlistAffected;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
        }   

      

        public int RemoveProductFromWishlist(int UserID, int ProductID)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "DELETE FROM dbo.Wishlist WHERE WishlistUserID = @UserID AND WishlistProductID = @ProductID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", UserID);
            dbCommand.Parameters.AddWithValue("@ProductID", ProductID);
            try
            {
                dbConnection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                //dbConnection.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
            }


        public List<ProductModel> GetWishListProducts(int wishlistUserID)
        {
            List<ProductModel> products = new List<ProductModel>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT * FROM dbo.Wishlist INNER JOIN dbo.Products ON dbo.Wishlist.WishlistProductID = dbo.Products.ProductID WHERE WishlistUserID = @UserID";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", wishlistUserID);

            try
            {
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();

                while (dbReader.Read())
                {
                    ProductModel product = new ProductModel();

                    product.ProductID = (int)dbReader["ProductID"];
                    product.ProductName = dbReader["ProductName"].ToString();
                    product.ProductDescription = dbReader["ProductDescription"].ToString();
                    product.ProductPrice = (int)dbReader["ProductPrice"];
                    product.ProductImage = dbReader["ProductImage"].ToString();
                    product.ProductCategoryID = (int)dbReader["ProductCategoryID"];

                    products.Add(product);
                }
                dbConnection.Close();
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }




        }


    }
    }
