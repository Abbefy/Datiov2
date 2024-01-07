using System.Data;
using System.Data.SqlClient;
//using Microsoft.Data.SqlClient;
using Datiov2.Data;
using Datiov2.Models;
using System;



namespace Datiov2.Models
{
    public class UserMethods
    {
        //public readonly ILogger<UserMethods> _logger;

        //public UserMethods(ILogger<UserMethods> logger)
        //{
        //    _logger = logger;

        //}


        public int Register(UserModel user, out string error)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "INSERT INTO dbo.Users (UserName, UserFirstName, UserLastName, UserPass, UserEmail, UserType) VALUES (@UserName, @UserFirstName, @UserLastName, @UserPass, @UserEmail, @UserType)";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserName", user.UserName);
            dbCommand.Parameters.AddWithValue("@UserFirstName", user.UserFirstName);
            dbCommand.Parameters.AddWithValue("@UserLastName", user.UserLastName);
            dbCommand.Parameters.AddWithValue("@UserPass", user.UserPass);
            dbCommand.Parameters.AddWithValue("@UserEmail", user.UserEmail);
            dbCommand.Parameters.AddWithValue("@UserType", user.UserType);

            try
            {
                dbConnection.Open();
                int insert = dbCommand.ExecuteNonQuery();
                error = "";
                return insert;
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

        public int GetUserID(string userName, out string error)
        {
            int userID = 0;
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT UserID FROM dbo.Users WHERE UserName = @UserName";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserName", userName);

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userID = (int)reader["UserID"];
                    }
                    error = "";
                    return userID;
                }
                else
                {
                    error = "Invalid username or password";
                    return 0;
                }
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

        public UserModel Login(string userName, string userPass, out string error)
        {
            UserModel user = new UserModel();
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT * FROM dbo.Users WHERE (UserName = @UserName OR UserEmail = @UserName) AND UserPass = @UserPass";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserName", userName);
            dbCommand.Parameters.AddWithValue("@UserPass", userPass);

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.UserID = (int)reader["UserID"];
                        user.UserName = reader["UserName"].ToString();
                        user.UserFirstName = reader["UserFirstName"].ToString();
                        user.UserLastName = reader["UserLastName"].ToString();
                        user.UserPass = reader["UserPass"].ToString();
                        user.UserEmail = reader["UserEmail"].ToString();
                        user.UserType = (int)reader["UserType"];
                    }
                    error = "";
                    Console.WriteLine("User logged innnnnnnnadadadxzcewfwc2123131231");
                    return user;
                }
                else
                {
                    error = "Invalid username or password";
                    return null;
                }
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

        public UserModel GetAccount(int userID, out string error)
        {
            UserModel user = new UserModel();
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "SELECT * FROM dbo.Users WHERE UserID = @UserID";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", userID);

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.UserID = (int)reader["UserID"];
                        user.UserName = reader["UserName"].ToString();
                        user.UserFirstName = reader["UserFirstName"].ToString();
                        user.UserLastName = reader["UserLastName"].ToString();
                        user.UserPass = reader["UserPass"].ToString();
                        user.UserEmail = reader["UserEmail"].ToString();
                        user.UserType = (int)reader["UserType"];
                    }
                    error = "";
                    return user;
                }
                else
                {
                    error = "Invalid username or password";
                    return null;
                }
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

        public int UpdateAccount(UserModel user, out string error)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";

            string sqlString = "UPDATE dbo.Users SET UserName = @UserName, UserFirstName = @UserFirstName, UserLastName = @UserLastName, UserPass = @UserPass, UserEmail = @UserEmail WHERE UserID = @UserID";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.AddWithValue("@UserID", user.UserID);
            dbCommand.Parameters.AddWithValue("@UserName", user.UserName);
            dbCommand.Parameters.AddWithValue("@UserFirstName", user.UserFirstName);
            dbCommand.Parameters.AddWithValue("@UserLastName", user.UserLastName);
            dbCommand.Parameters.AddWithValue("@UserPass", user.UserPass);
            dbCommand.Parameters.AddWithValue("@UserEmail", user.UserEmail);

            try
            {
                dbConnection.Open();
                int update = dbCommand.ExecuteNonQuery();
                error = "";
                return update;
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

        public int DeleteAccount(int userID, out string error)
        {
            int rowsAffected = 0;
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "DELETE FROM dbo.Users WHERE UserID = @UserID";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbCommand.Parameters.AddWithValue("@UserID", userID);
            try
            {
                dbConnection.Open();
                rowsAffected = dbCommand.ExecuteNonQuery();
                error = "";
                return rowsAffected;
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



           


    }
}
