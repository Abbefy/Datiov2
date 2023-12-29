using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Datiov2.Models; // Ensure this is the correct namespace for ProductModel
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Data.SqlClient;


namespace Datiov2.Models
{
    public class UserMethods
    {
        public void AddUser(UserModel user)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Abbesdb; Integrated Security = True";
            string sqlString = "INSERT INTO Users (UserName, UserFirstName, UserLastName, UserPass, UserEmail, UserType) VALUES (@UserName, @UserFirstName, @UserLastName, @UserPass, @UserEmail, @UserType)";

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
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }

        }


    }
}
