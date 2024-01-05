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
using System.Data.SqlTypes;



//namespace Datiov2.Models
//{
//    public class OrderModel
//    {
//        public int OrderID { get; set; }
//        public int OrderUserID { get; set; }
//        public int OrderPrice { get; set; }
//        public string OrderAddress { get; set; }
//        public string OrderFirstName { get; set; }
//        public string OrderLastName { get; set; }
//        public int OrderPostalCode { get; set; }
//        public string OrderCity { get; set; }
//        public string OrderDate { get; set; }

//    }
//}

//CREATE TABLE[dbo].[Orders] (
//    [OrderID]         INT IDENTITY(1, 1) NOT NULL,
//    [OrderUserID]     INT            NOT NULL,
//    [OrderPrice]      INT            NOT NULL,
//    [OrderAddress]    NVARCHAR (100) NOT NULL,

//    [OrderFirstName]  NVARCHAR (100) NOT NULL,

//    [OrderLastName]   NVARCHAR (100) NOT NULL,
//    [OrderPostalCode] INT            NOT NULL,
//    [OrderCity]       NVARCHAR (100) NOT NULL,
//    [OrderDate]       DATETIME       DEFAULT (getdate()) NULL,
//    PRIMARY KEY CLUSTERED ([OrderID] ASC),
//    FOREIGN KEY([OrderUserID]) REFERENCES[dbo].[Users]([UserID])
//);


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
using System.Data.SqlTypes;



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


//namespace Datiov2.Models
//{
//    public class CartMethods
//    {

//        public List<CartItemModel> GetCartItems(int cartID)
//        {
//            List<CartItemModel> cartItems = new List<CartItemModel>();

//            using (SqlConnection dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Abbesdb; Integrated Security=True"))
//            {
//                string sqlString = @"
//            SELECT ci.*, p.ProductName, p.ProductPrice, p.ProductImage, p.ProductStock 
//            FROM dbo.CartItem ci
//            INNER JOIN dbo.Products p ON ci.CartItemProductID = p.ProductID
//            WHERE ci.CartItemCartID = @CartID";

//                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
//                dbCommand.Parameters.AddWithValue("@CartID", cartID);

//                dbConnection.Open();
//                using (SqlDataReader dbReader = dbCommand.ExecuteReader())
//                {
//                    while (dbReader.Read())
//                    {
//                        CartItemModel cartItem = new CartItemModel
//                        {
//                            CartItemID = dbReader.GetInt32(dbReader.GetOrdinal("CartItemID")),
//                            CartItemCartID = dbReader.GetInt32(dbReader.GetOrdinal("CartItemCartID")),
//                            CartItemProductID = dbReader.GetInt32(dbReader.GetOrdinal("CartItemProductID")),
//                            CartItemQuantity = dbReader.GetInt32(dbReader.GetOrdinal("CartItemQuantity")),
//                            CartItemPrice = dbReader.GetInt32(dbReader.GetOrdinal("CartItemPrice")),
//                            CartProduct = new ProductModel
//                            {
//                                ProductName = dbReader.GetString(dbReader.GetOrdinal("ProductName")),
//                                ProductPrice = dbReader.GetInt32(dbReader.GetOrdinal("ProductPrice")),
//                                ProductImage = dbReader.GetString(dbReader.GetOrdinal("ProductImage")),
//                                ProductStock = dbReader.GetInt32(dbReader.GetOrdinal("ProductStock"))


//                            }
//                        };
//                        cartItems.Add(cartItem);
//                    }
//                }
//            }

//            return cartItems;
//        }



//        public CartModel GetCart(int cartID)
//        {
//            CartModel cart = new CartModel();

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
//            string sqlString = "SELECT * FROM dbo.Cart WHERE CartID = @CartID";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
//            dbConnection.Open();
//            dbCommand.Parameters.AddWithValue("@CartID", cartID);
//            dbCommand.ExecuteNonQuery();
//            return cart;
//        }

//        public int GetCartID(int userID)
//        {
//            int cartID = 0;

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
//            string sqlString = "SELECT * FROM dbo.Cart WHERE CartUserID = @UserID";



//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
//            try
//            {
//                dbConnection.Open();
//                dbCommand.Parameters.AddWithValue("@UserID", userID);

//                cartID = (int)dbCommand.ExecuteScalar();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }
//            return cartID;

//        }

//        public int AddToCart(int cartID, int productID, int quantity, int price)
//        {
//            int rowsAffected = 0;

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
//            string sqlString = "INSERT INTO dbo.CartItem (CartItemCartID, CartItemProductID, CartItemQuantity, CartItemPrice) VALUES (@CartID, @ProductID, @Quantity, @Price)";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//            dbCommand.Parameters.AddWithValue("@CartID", cartID);
//            dbCommand.Parameters.AddWithValue("@ProductID", productID);
//            dbCommand.Parameters.AddWithValue("@Quantity", quantity);
//            dbCommand.Parameters.AddWithValue("@Price", price);

//            try
//            {
//                dbConnection.Open();
//                rowsAffected = dbCommand.ExecuteNonQuery();
//                dbConnection.Close();
//                return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }
//        }

//        public int CreateCart(int userID)
//        {
//            int cartID = 0;

//            using (SqlConnection dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Abbesdb;Integrated Security=True"))
//            {
//                string sqlString = "INSERT INTO dbo.Cart (CartUserID) VALUES (@UserID); SELECT SCOPE_IDENTITY();";

//                using (SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection))
//                {
//                    dbCommand.Parameters.AddWithValue("@UserID", userID);

//                    try
//                    {
//                        dbConnection.Open();
//                        // Use ExecuteScalar to get the last inserted identity value.
//                        cartID = Convert.ToInt32(dbCommand.ExecuteScalar());
//                    }
//                    catch (SqlException ex)
//                    {
//                        // Log exception
//                        // Display or return user-friendly error
//                    }
//                }
//            }
//            return cartID;
//        }



//        //public int CreateCart(int userID)
//        //{
//        //    int cartID = 0;
//        //    int rowsAffected = 0;

//        //    SqlConnection dbConnection = new SqlConnection();
//        //    dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
//        //    string sqlString = "INSERT INTO dbo.Cart (CartUserID) VALUES (@UserID)";

//        //    SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//        //    dbCommand.Parameters.AddWithValue("@UserID", userID);

//        //    try
//        //    {
//        //        dbConnection.Open();
//        //        rowsAffected = dbCommand.ExecuteNonQuery();
//        //        dbConnection.Close();
//        //        cartID = GetCartID(userID);
//        //        return cartID;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //    finally
//        //    {
//        //        dbConnection.Close();
//        //    }
//        //}

//        public void DeleteCartItem(int cartItemID)
//        {
//            //int rowsAffected = 0;

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//            string sqlString = "DELETE FROM dbo.CartItem WHERE CartItemID = @CartItemID";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//            dbCommand.Parameters.AddWithValue("@CartItemID", cartItemID);

//            try
//            {
//                dbConnection.Open();
//                //rowsAffected = dbCommand.ExecuteNonQuery();
//                dbCommand.ExecuteNonQuery();
//                dbConnection.Close();
//                //return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }

//        }

//        public int CheckForProductInCart(int cartID, int productID)
//        {
//            int rowsAffected = 0;

//            SqlConnection dbConnection = new SqlConnection();

//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
//            string sqlString = "SELECT * FROM dbo.CartItem WHERE CartItemCartID = @CartID AND CartItemProductID = @ProductID";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//            dbCommand.Parameters.AddWithValue("@CartID", cartID);
//            dbCommand.Parameters.AddWithValue("@ProductID", productID);

//            try
//            {
//                dbConnection.Open();
//                rowsAffected = dbCommand.ExecuteNonQuery();
//                dbConnection.Close();
//                return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }
//        }

//        public int GetCartItemID(int cartID, int productID)
//        {
//            int cartItemID = 0;

//            SqlConnection dbConnection = new SqlConnection();

//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
//            string sqlString = "SELECT * FROM dbo.CartItem WHERE CartItemCartID = @CartID AND CartItemProductID = @ProductID";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//            dbCommand.Parameters.AddWithValue("@CartID", cartID);
//            dbCommand.Parameters.AddWithValue("@ProductID", productID);

//            try
//            {
//                dbConnection.Open();
//                SqlDataReader reader = dbCommand.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    while (reader.Read())
//                    {
//                        cartItemID = (int)reader["CartItemID"];
//                    }
//                    return cartItemID;
//                }
//                else
//                {
//                    return 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }

//        }




//        public void UpdateCartItemQuantity(int cartItemID, int quantityToAdd)
//        {

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//            string sqlString = "UPDATE dbo.CartItem SET CartItemQuantity = CartItemQuantity + @QuantityToAdd WHERE CartItemID = @CartItemID";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//            dbCommand.Parameters.AddWithValue("@CartItemID", cartItemID);
//            dbCommand.Parameters.AddWithValue("@quantityToAdd", quantityToAdd);

//            try
//            {
//                dbConnection.Open();
//                dbCommand.ExecuteNonQuery();
//                //return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }

//        }

//        public void UpdateCartItemQuantityCheckout(int cartItemID, int quantityToAdd)
//        {

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//            string sqlString = "UPDATE dbo.CartItem SET CartItemQuantity = @QuantityToAdd WHERE CartItemID = @CartItemID";

//            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

//            dbCommand.Parameters.AddWithValue("@CartItemID", cartItemID);
//            dbCommand.Parameters.AddWithValue("@quantityToAdd", quantityToAdd);

//            try
//            {
//                dbConnection.Open();
//                dbCommand.ExecuteNonQuery();
//                //return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }

//        }



//        public void UpdateCartTotal(int cartID)
//        {
//            int cartTotal = 0;
//            int cartItemCount = 0;

//            SqlConnection dbConnection = new SqlConnection();
//            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

//            SqlCommand command = new SqlCommand();
//            command.Connection = dbConnection;
//            command.CommandText = "SELECT * FROM CartItem WHERE CartItemCartID = @cartID";
//            command.Parameters.AddWithValue("@cartID", cartID);
//            command.ExecuteNonQuery();

//            SqlDataReader reader = command.ExecuteReader();

//            while (reader.Read())
//            {
//                cartTotal += Convert.ToInt32(reader["CartItemPrice"]);
//                cartItemCount++;
//            }

//            reader.Close();

//            command.CommandText = "UPDATE Cart SET CartTotal = @cartTotal, CartItemCount = @cartItemCount WHERE CartID = @cartID";
//            command.Parameters.AddWithValue("@cartTotal", cartTotal);
//            command.Parameters.AddWithValue("@cartItemCount", cartItemCount);
//            command.ExecuteNonQuery();
//        }





//    }

//}






namespace Datiov2.Models
{
    public class OrderMethods
    {

        public List<OrderModel> GetOrders(int userID)
        {
            List<OrderModel> orders = new List<OrderModel>();

            using (SqlConnection dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Abbesdb; Integrated Security=True"))
            {
                string sqlString = @"
            SELECT * FROM dbo.Orders WHERE OrderUserID = @UserID";

                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
                dbCommand.Parameters.AddWithValue("@UserID", userID);

                dbConnection.Open();
                using (SqlDataReader dbReader = dbCommand.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        OrderModel order = new OrderModel
                        {
                            OrderID = dbReader.GetInt32(dbReader.GetOrdinal("OrderID")),
                            OrderCartID = dbReader.GetInt32(dbReader.GetOrdinal("OrderCartID")),
                            OrderUserID = dbReader.GetInt32(dbReader.GetOrdinal("OrderUserID")),
                            OrderPrice = dbReader.GetInt32(dbReader.GetOrdinal("OrderPrice")),
                            OrderAddress = dbReader.GetString(dbReader.GetOrdinal("OrderAddress")),
                            OrderFirstName = dbReader.GetString(dbReader.GetOrdinal("OrderFirstName")),
                            OrderLastName = dbReader.GetString(dbReader.GetOrdinal("OrderLastName")),
                            OrderPostalCode = dbReader.GetInt32(dbReader.GetOrdinal("OrderPostalCode")),
                            OrderCity = dbReader.GetString(dbReader.GetOrdinal("OrderCity")),
                            OrderDate = dbReader.GetDateTime(dbReader.GetOrdinal("OrderDate")).ToString("MMMM dd, yyyy")

                        };
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public OrderModel GetOrder(int orderID)
        {
            OrderModel order = new OrderModel();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "SELECT * FROM dbo.Orders WHERE OrderID = @OrderID";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbConnection.Open();
            dbCommand.Parameters.AddWithValue("@OrderID", orderID);
            dbCommand.ExecuteNonQuery();
            return order;
        }

        public int CreateOrder(int userID, int cartID, int orderPrice, string orderAddress, string orderFirstName, string orderLastName, int orderPostalCode, string orderCity)
        {
            int orderID = 0;
            int rowsAffected = 0;

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "INSERT INTO dbo.Orders (OrderUserID, OrderCartID, OrderPrice, OrderAddress, OrderFirstName, OrderLastName, OrderPostalCode, OrderCity) VALUES (@UserID, @cartID, @OrderPrice, @OrderAddress, @OrderFirstName, @OrderLastName, @OrderPostalCode, @OrderCity); SELECT SCOPE_IDENTITY();";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", userID);
            dbCommand.Parameters.AddWithValue("@cartID", cartID);
            dbCommand.Parameters.AddWithValue("@OrderPrice", orderPrice);
            dbCommand.Parameters.AddWithValue("@OrderAddress", orderAddress);
            dbCommand.Parameters.AddWithValue("@OrderFirstName", orderFirstName);
            dbCommand.Parameters.AddWithValue("@OrderLastName", orderLastName);
            dbCommand.Parameters.AddWithValue("@OrderPostalCode", orderPostalCode);
            dbCommand.Parameters.AddWithValue("@OrderCity", orderCity);

            try
            {
                dbConnection.Open();
                // Use ExecuteScalar to get the last inserted identity value.
                orderID = Convert.ToInt32(dbCommand.ExecuteScalar());
                dbConnection.Close();
                return orderID;
            }
            catch (SqlException ex)
            {
                // Log exception
                // Display or return user-friendly error
            }
            return orderID;
        }

        public void TransferCartItemsToOrderDetailsOG(int OrderID, int cartID)
        {
            List<CartItemModel> cartItems = new List<CartItemModel>();
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string getCartItemsString = "SELECT * FROM dbo.CartItem WHERE CartItemCartID = @CartID";

            SqlCommand dbCommand = new SqlCommand(getCartItemsString, dbConnection);

            dbCommand.Parameters.AddWithValue("@CartID", cartID);

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cartItems.Add(new CartItemModel
                        {
                            CartItemID = (int)reader["CartItemID"],
                            CartItemProductID = (int)reader["CartItemProductID"],
                            CartItemQuantity = (int)reader["CartItemQuantity"],
                            CartItemPrice = (int)reader["CartItemPrice"]
                        });
                    }
                }
                else
                {
                    //return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }

            //using (SqlCommand getCartItemsCommand = new SqlCommand(getCartItemsString, dbConnection))
            //{
            //    getCartItemsCommand.Parameters.AddWithValue("@CartID", cartID);
            //    dbConnection.Open();
            //    using (SqlDataReader reader = getCartItemsCommand.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            cartItems.Add(new CartItemModel
            //            {
            //                CartItemID = (int)reader["CartItemID"],
            //                CartItemProductID = (int)reader["CartItemProductID"],
            //                CartItemQuantity = (int)reader["CartItemQuantity"],
            //                CartItemPrice = (int)reader["CartItemPrice"]
            //            });
            //        }

            //    }
            //    dbConnection.Close();
            //}

            foreach (var item in cartItems)
            {
                //cartItems.Remove(item);


                string insertOrderDetailsString = "INSERT INTO dbo.OrderDetails (OrderDetailsOrderID, OrderDetailsProductID, OrderDetailsQuantity, OrderDetailsPrice) VALUES (@OrderID, @ProductID, @Quantity, @Price)";
                SqlCommand dbCommand2 = new SqlCommand(insertOrderDetailsString, dbConnection);

                dbCommand2.Parameters.AddWithValue("@OrderID", OrderID);
                dbCommand2.Parameters.AddWithValue("@ProductID", item.CartItemProductID);
                dbCommand2.Parameters.AddWithValue("@Quantity", item.CartItemQuantity);
                dbCommand2.Parameters.AddWithValue("@Price", item.CartItemPrice);

                dbConnection.Open();
                dbCommand2.ExecuteNonQuery();
                dbConnection.Close();



                //using (SqlCommand insertOrderDetailsCommand = new SqlCommand(insertOrderDetailsString, dbConnection))
                //{
                //    insertOrderDetailsCommand.Parameters.AddWithValue("@OrderID", OrderID);
                //    insertOrderDetailsCommand.Parameters.AddWithValue("@ProductID", item.CartItemProductID);
                //    insertOrderDetailsCommand.Parameters.AddWithValue("@Quantity", item.CartItemQuantity);
                //    insertOrderDetailsCommand.Parameters.AddWithValue("@Price", item.CartItemPrice);

                //    dbConnection.Open();
                //    insertOrderDetailsCommand.ExecuteNonQuery();
                //    dbConnection.Close();
                //}
            }

        }


        public void TransferCartItemsToOrderDetailsWORK(int OrderID, int cartID)
        {
            List<CartItemModel> cartItems = new List<CartItemModel>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string getCartItemsString = "SELECT * FROM dbo.CartItem WHERE CartItemCartID = @CartID";
            SqlCommand dbCommand = new SqlCommand(getCartItemsString, dbConnection);
            dbCommand.Parameters.AddWithValue("@CartID", cartID);

            try
            {
                dbConnection.Open();
                using (SqlDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cartItems.Add(new CartItemModel
                        {
                            CartItemID = (int)reader["CartItemID"],
                            CartItemProductID = (int)reader["CartItemProductID"],
                            CartItemQuantity = (int)reader["CartItemQuantity"],
                            CartItemPrice = (int)reader["CartItemPrice"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
            finally
            {
                dbConnection.Close();
            }

            foreach (var item in cartItems)
            {
                string insertOrderDetailsString = "INSERT INTO dbo.OrderDetails (OrderDetailsOrderID, OrderDetailsProductID, OrderDetailsQuantity, OrderDetailsPrice) VALUES (@OrderID, @ProductID, @Quantity, @Price)";

                SqlCommand insertCommand = new SqlCommand(insertOrderDetailsString, dbConnection);
                insertCommand.Parameters.AddWithValue("@OrderID", OrderID);
                insertCommand.Parameters.AddWithValue("@ProductID", item.CartItemProductID);
                insertCommand.Parameters.AddWithValue("@Quantity", item.CartItemQuantity);
                insertCommand.Parameters.AddWithValue("@Price", item.CartItemPrice);

                try
                {
                    dbConnection.Open();
                    insertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }


        public void TransferCartItemsToOrderDetails(int OrderID, int cartID)
        {
            List<CartItemModel> cartItems = new List<CartItemModel>();
            SqlConnection dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Abbesdb;Integrated Security=True");

            string getCartItemsString = "SELECT * FROM dbo.CartItem WHERE CartItemCartID = @CartID";

            using (SqlCommand getCartItemsCommand = new SqlCommand(getCartItemsString, dbConnection))
            {
                getCartItemsCommand.Parameters.AddWithValue("@CartID", cartID);
                dbConnection.Open();
                using (SqlDataReader reader = getCartItemsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cartItems.Add(new CartItemModel
                        {
                            CartItemID = (int)reader["CartItemID"],
                            CartItemProductID = (int)reader["CartItemProductID"],
                            CartItemQuantity = (int)reader["CartItemQuantity"],
                            CartItemPrice = (int)reader["CartItemPrice"]
                        });
                    }
                }
                dbConnection.Close();
            }

            foreach (var item in cartItems)
            {
                string insertOrderDetailsString = "INSERT INTO dbo.OrderDetails (OrderDetailsOrderID, OrderDetailsProductID, OrderDetailsQuantity, OrderDetailsPrice) VALUES (@OrderID, @ProductID, @Quantity, @Price)";
                using (SqlCommand insertOrderDetailsCommand = new SqlCommand(insertOrderDetailsString, dbConnection))
                {
                    insertOrderDetailsCommand.Parameters.AddWithValue("@OrderID", OrderID);
                    insertOrderDetailsCommand.Parameters.AddWithValue("@ProductID", item.CartItemProductID);
                    insertOrderDetailsCommand.Parameters.AddWithValue("@Quantity", item.CartItemQuantity);
                    insertOrderDetailsCommand.Parameters.AddWithValue("@Price", item.CartItemPrice);

                    dbConnection.Open();
                    insertOrderDetailsCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
        }










    }
}
