using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Domain.Model;
using System.Data.SqlClient;

namespace Rage.Addservice.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string connectionString = "Server=(local);Database=SuperServer; User Id=pk; Password=pk;";
        
        public List<User> GetUsers()
        {
            try
            {
                string query = @"SELECT usrId, usrName, usrEmail, usrLogin, usrPassword
                            FROM Users";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<User> users = new List<User>();

                        while (dr.Read())
                        {
                            users.Add(new User()
                            {
                                Id = (dr[0] == null || dr[0] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr[0]),
                                Name = (dr[1] == null || dr[1] == DBNull.Value) ? null : Convert.ToString(dr[1]),
                                Email = (dr[2] == null || dr[2] == DBNull.Value) ? null : Convert.ToString(dr[2]),
                                Login = (dr[3] == null || dr[3] == DBNull.Value) ? null : Convert.ToString(dr[3]),
                                Password = (dr[4] == null || dr[4] == DBNull.Value) ? null : Convert.ToString(dr[4])
                            });
                        }

                        return users.Count > 0 ? users : null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetUsers: " + ex.Message);
            }
        }

        public User Login(string login, string pass)
        {
            try
            {
                string query = @"SELECT usrId, usrName, usrEmail
                            FROM Users
                            WHERE usrLogin = @login and usrPassword = @pass";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new User()
                            {
                                Id = (dr[0] == null || dr[0] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr[0]),
                                Login = login,
                                Password = pass,
                                Name = (dr[1] == null || dr[1] == DBNull.Value) ? null : Convert.ToString(dr[1]),
                                Email = (dr[2] == null || dr[2] == DBNull.Value) ? null : Convert.ToString(dr[2])
                            };
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error login: " + ex.Message);
            }
        }

        public int? CreateUser(User user)
        {
            try
            {
                string query = @"INSERT INTO Users (usrLogin, usrPassword, usrEmail, usrName)
                            VALUES (@login, @pass, @email, @name);
                            SELECT CAST(scope_identity() AS int)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", user.Name);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@name", user.Name);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            return dr[0] == null || dr[0] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[0]);
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error CreateUser: " + ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                string query = @"UPDATE Users SET usrLogin = @login, usrPassword = @pass, usrEmail = @email, usrName = @name
                                 WHERE usrId = @usrId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@usrId", user.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error UpdateUser: " + ex.Message);
            }
        }
    }
}
