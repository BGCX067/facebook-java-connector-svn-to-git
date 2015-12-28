using System;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rage.Addservice.Domain.Model;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        public Advert Advert()
        {
            var advert = new Advert
                             {
                                 Description = "Wassup",
                                 CreationTime = DateTime.Now,
                                 Id = 1,
                                 Name = "Cos tam",
                                 Post = "Test",
                                 Wall = new Wall
                                            {
                                                Id = 1,
                                                Name = "Moj"
                                            }
                             };
            return advert;
        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        
           

        //}
        [TestMethod]
        public void GetUsers()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.GetUsers();
        }
        [TestMethod]
        public void Login()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.Login("Test","testowe,.123");
        }

        [TestMethod]
        public void IsLoggedin()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.IsLoggedIn();
        }

        [TestMethod]
        public void CreateUser()
        {
            var user = new User
                           {
                               Login = "Test",
                               Name = "Łukasz",
                               Email = "kurkey@mars.iti.pk.edu.pl",
                               Password = "testowe,.123"
                           };
            var client = new ServiceReference1.PersistenceServiceClient();
            client.CreateUser(user);
        }

        [TestMethod]
        public void UpdateUser()
        {
            var user = new User
            {
                Login = "Test",
                Name = "Tomek",
                Email = "kurkey@mars.iti.pk.edu.pl",
                Password = "testowe,.123"
            };
            var client = new ServiceReference1.PersistenceServiceClient();
            client.UpdateUser(user);
        }
        [TestMethod]
        public void CreateAdvert()
        {
            var advert = Advert();
            var client = new ServiceReference1.PersistenceServiceClient();
            var result = client.CreateAdvert(advert);
            //Assert.AreEqual(advert.Id, result.Id, "Objects are equal");
           
          
        }
        [TestMethod]
        public void GetAdverts()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.GetAdverts();
        }

        [TestMethod]
        public void GetAdvert()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.GetAdvert(1);
        }

        [TestMethod]
        public void GetAdvertStatus()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.GetAdvertStatus(1);
        }

        [TestMethod]
        public void GetWalls()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.GetWalls();
        }

        [TestMethod]
        public void GetWall()
        {
            var client = new ServiceReference1.PersistenceServiceClient();
            client.GetWall(1);
        }

        [TestMethod]
        public int? TestDbConnection()
        {
            string connectionString =
                "Server=(local);Database=SuperServer;Integrated Security=SSPI;Trusted_Connection=True;";
            bool can = false;
            try
            {
                string query =
                    @"INSERT INTO Advert (   advName, advDescription, advUsrId)
                                            VALUES (  @advName, @advDescription, @advUsrId);
                                            SELECT CAST(scope_identity() AS int)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // cmd.Parameters.AddWithValue("@advAtt", DBNull.Value); //cmd.Parameters.AddWithValue("@advAtt", advert.Attachment);
                    // cmd.Parameters.AddWithValue("@advAttType", advert.Attachment_Type);
                    cmd.Parameters.AddWithValue("@advName", "testowa");
                    cmd.Parameters.AddWithValue("@advDescription", "kampania na rzecz testu");
                    cmd.Parameters.AddWithValue("@advUsrId", "2");

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            return dr[0] == null || dr[0] == DBNull.Value ? (int?) null : Convert.ToInt32(dr[0]);
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error CreateAdvert: " + ex.Message);
            }



        }
    }
}
