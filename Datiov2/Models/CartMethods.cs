using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Datiov2.Models;
using System.Data.Common;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis;
using datiov2.models;
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

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT * FROM dbo.CartItem WHERE CartItemCartID = @CartID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@CartID", cartID);

            try
            {
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();

                while (dbReader.Read())
                {
                    CartItemModel cartItem = new CartItemModel();

                    cartItem.CartItemID = (int)dbReader["CartItemID"];
                    cartItem.CartItemCartID = (int)dbReader["CartItemCartID"];
                    cartItem.CartItemProductID = (int)dbReader["CartItemProductID"];
                    cartItem.CartItemQuantity = (int)dbReader["CartItemQuantity"];
                    cartItem.CartItemPrice = (int)dbReader["CartItemPrice"];

                    cartItems.Add(cartItem);
                }
                dbConnection.Close();
                return cartItems;
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
            dbConnection.Open();
            dbCommand.Parameters.AddWithValue("@UserID", userID);
            dbCommand.ExecuteNonQuery();
            return cartID;
        }




        public void AddToCart(int cartID, int productID, int quantity)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "INSERT INTO CartItem (CartItemCartID, CartItemProductID, CartItemQuantity) VALUES (@cartID, @productID, @quantity)";
            command.Parameters.AddWithValue("@cartID", cartID);
            command.Parameters.AddWithValue("@productID", productID);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.ExecuteNonQuery();
        }

        public void RemoveFromCart(int cartItemID)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "DELETE FROM CartItem WHERE CartItemID = @cartItemID";
            command.Parameters.AddWithValue("@cartItemID", cartItemID);
            command.ExecuteNonQuery();
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


         