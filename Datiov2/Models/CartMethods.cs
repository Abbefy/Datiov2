using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Datiov2.Models;
using System.Data.Common;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



//CREATE TABLE[dbo].[Cart] (
//    [CartID]     INT IDENTITY(1, 1) NOT NULL,
//    [CartUserID] INT NOT NULL,
//    PRIMARY KEY CLUSTERED ([CartID] ASC),
//    FOREIGN KEY([CartUserID]) REFERENCES[dbo].[Users]([UserID])
//);

//CREATE TABLE[dbo].[CartItem] (
//    [CartItemID]        INT IDENTITY(1, 1) NOT NULL,
//    [CartItemCartID]    INT NOT NULL,
//    [CartItemProductID] INT NOT NULL,
//    [CartItemQuantity]  INT NOT NULL,
//    [CartItemPrice]     INT NOT NULL,
//    PRIMARY KEY CLUSTERED ([CartItemID] ASC),
//    FOREIGN KEY([CartItemID]) REFERENCES[dbo].[Cart]([CartID]),
//    FOREIGN KEY([CartItemProductID]) REFERENCES[dbo].[Products]([ProductID])
//);

//CREATE TABLE[dbo].[Products] (
//    [ProductID]             INT IDENTITY(1, 1) NOT NULL,
//    [ProductName]           NVARCHAR (100)  NOT NULL,
//    [ProductImage]          NVARCHAR (1000) NULL,
//    [ProductDescription]    NVARCHAR (MAX)  NULL,
//    [ProductPrice]          INT             NOT NULL,
//    [ProductStock]          INT             NOT NULL,
//    [ProductCategoryID]     INT             NULL,
//    [ProductSpecifications] NVARCHAR (MAX)  NULL,
//    PRIMARY KEY CLUSTERED ([ProductID] ASC),
//    FOREIGN KEY([ProductCategoryID]) REFERENCES[dbo].[Categories]([CategoryID])
//);


namespace Datiov2.Models
{
    public class CartMethods
    {

        public List<CartItemModel> GetCartItems(int cartID)
        {
            List<CartItemModel> cartItems = new List<CartItemModel>();

            using (SqlConnection dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Abbesdb; Integrated Security=True"))
            {
                string sqlString = @"
            SELECT ci.*, p.ProductName, p.ProductPrice, p.ProductImage, p.ProductStock 
            FROM dbo.CartItem ci
            INNER JOIN dbo.Products p ON ci.CartItemProductID = p.ProductID
            WHERE ci.CartItemCartID = @CartID";

                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
                dbCommand.Parameters.AddWithValue("@CartID", cartID);

                dbConnection.Open();
                using (SqlDataReader dbReader = dbCommand.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        CartItemModel cartItem = new CartItemModel
                        {
                            CartItemID = dbReader.GetInt32(dbReader.GetOrdinal("CartItemID")),
                            CartItemCartID = dbReader.GetInt32(dbReader.GetOrdinal("CartItemCartID")),
                            CartItemProductID = dbReader.GetInt32(dbReader.GetOrdinal("CartItemProductID")),
                            CartItemQuantity = dbReader.GetInt32(dbReader.GetOrdinal("CartItemQuantity")),
                            CartItemPrice = dbReader.GetInt32(dbReader.GetOrdinal("CartItemPrice")),
                            CartProduct = new ProductModel
                            {
                                ProductName = dbReader.GetString(dbReader.GetOrdinal("ProductName")),
                                ProductPrice = dbReader.GetInt32(dbReader.GetOrdinal("ProductPrice")),
                                ProductImage = dbReader.GetString(dbReader.GetOrdinal("ProductImage")),
                                ProductStock = dbReader.GetInt32(dbReader.GetOrdinal("ProductStock"))


                            }
                        };
                        cartItems.Add(cartItem);
                    }
                }
            }

            return cartItems;
        }



        public CartModel GetCart(int cartID)
        {
            CartModel cart = new CartModel();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "SELECT * FROM dbo.Cart WHERE CartID = @CartID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbConnection.Open();
            dbCommand.Parameters.AddWithValue("@CartID", cartID);
            dbCommand.ExecuteNonQuery();
            return cart;
            }

        public int GetCartID(int userID)
        {
            int cartID = 0;

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "SELECT * FROM dbo.Cart WHERE CartUserID = @UserID";



            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            try
            {
                dbConnection.Open();
                dbCommand.Parameters.AddWithValue("@UserID", userID);

                cartID = (int)dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }
            return cartID;

        }

        public int AddToCart(int cartID, int productID, int quantity, int price)
        {
            int rowsAffected = 0;

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "INSERT INTO dbo.CartItem (CartItemCartID, CartItemProductID, CartItemQuantity, CartItemPrice) VALUES (@CartID, @ProductID, @Quantity, @Price)";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@CartID", cartID);
            dbCommand.Parameters.AddWithValue("@ProductID", productID);
            dbCommand.Parameters.AddWithValue("@Quantity", quantity);
            dbCommand.Parameters.AddWithValue("@Price", price);

            try
            {
                dbConnection.Open();
                rowsAffected = dbCommand.ExecuteNonQuery();
                dbConnection.Close();
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



        public int CreateCart(int userID)
        {
            int cartID = 0;
            int rowsAffected = 0;

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "INSERT INTO dbo.Cart (CartUserID) VALUES (@UserID)";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", userID);

            try
            {
                dbConnection.Open();
                rowsAffected = dbCommand.ExecuteNonQuery();
                dbConnection.Close();
                cartID = GetCartID(userID);
                return cartID;
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

        public int RemoveFromCart(int cartItemID)
        {
            int rowsAffected = 0;

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "DELETE FROM dbo.CartItem WHERE CartItemID = @CartItemID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@CartItemID", cartItemID);

            try
            {
                dbConnection.Open();
                rowsAffected = dbCommand.ExecuteNonQuery();
                dbConnection.Close();
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
        
        public void UpdateCartItemQuantity(int cartItemID, int quantity)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "UPDATE CartItem SET CartItemQuantity = @quantity WHERE CartItemID = @cartItemID";
            command.Parameters.AddWithValue("@cartItemID", cartItemID);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.ExecuteNonQuery();
        }

        public void UpdateCartTotal(int cartID)
        {
            int cartTotal = 0;
            int cartItemCount = 0;

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "SELECT * FROM CartItem WHERE CartItemCartID = @cartID";
            command.Parameters.AddWithValue("@cartID", cartID);
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                cartTotal += Convert.ToInt32(reader["CartItemPrice"]);
                cartItemCount++;
            }

            reader.Close();

            command.CommandText = "UPDATE Cart SET CartTotal = @cartTotal, CartItemCount = @cartItemCount WHERE CartID = @cartID";
            command.Parameters.AddWithValue("@cartTotal", cartTotal);
            command.Parameters.AddWithValue("@cartItemCount", cartItemCount);
            command.ExecuteNonQuery();
        }



    }

}


         