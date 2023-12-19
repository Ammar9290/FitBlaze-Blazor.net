using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass; 

namespace DAL
{
    public class User
    {
        public void CreateUser(userModel user)
        {
            using (SqlConnection connection = DBhelper.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("CreateUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    cmd.ExecuteNonQuery();
                }
            }
        }





        public static void DeleteUserInfo(int id)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void UpdateStudentInformation(userModel ms)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", ms.Userid); // Assuming Userid is the unique identifier
                    cmd.Parameters.AddWithValue("@Username", ms.Name);
                    cmd.Parameters.AddWithValue("@Password", ms.Password);
                    cmd.Parameters.AddWithValue("@Email", ms.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public static List<userModel> GetUserInfo()
        {
            List<userModel> products = new List<userModel>();

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userModel product = new userModel
                            {
                                Userid = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Name = reader.GetString(reader.GetOrdinal("Username")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.IsDBNull(reader.GetOrdinal("Password"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("Password"))
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving user information: {ex.Message}");
            }

            return products;
        }

        public static userModel GetUserById(int userId)
        {
            userModel user = null;

            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUsersByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userId);

                // Assuming you are expecting a single row as the result
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new userModel
                        {
                            Userid = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Name = reader.GetString(reader.GetOrdinal("Username")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Password = reader.IsDBNull(reader.GetOrdinal("Password"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Password"))
                        };
                    }
                }
            }

            return user;
        }
    }
    }
