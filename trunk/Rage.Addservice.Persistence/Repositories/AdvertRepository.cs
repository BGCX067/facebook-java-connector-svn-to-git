using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Domain.Model;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Facebook;
using System.Dynamic;

namespace Rage.Addservice.Persistence.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        //private string userAccessToken = "AAAF1TOS9poYBAFFsHjNATwHP22cifoJ295i2rKfJVGycNAkSo4xCpZAJjkuy1CnZBuGEfddVxctk7veqc6nKN9yelasEWJwHbpCapPxWQMFlWLzYp5";
        private const string AppId = "410448092309126";
        private const string ExtendedPermissions = "user_about_me,publish_stream,manage_pages,offline_access";

        private string connectionString = "Server=(local);Database=SuperServer; User Id=pk; Password=pk;";

        public int? CreateAdvert(Advert advert, int userId)
        {
            //loginToFacebookUsingTestUser();
            //requestAndParseUserToken();
            int? advId = null;
            //return 2;
            try
            {
                string query = @"
                            INSERT INTO Advert (   advName, advDescription, advUsrId, advAtt, advAttType, advIsInTwitter)
                            VALUES (  @advName, @advDescription, @advUsrId, @advAtt, @advAttType, @advIsInTwitter);
                            SELECT CAST(scope_identity() AS int)";

                

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (advert.Attachment != null)                   
                        cmd.Parameters.Add("@advAtt", System.Data.SqlDbType.VarBinary).Value = advert.Attachment;
                    else
                        cmd.Parameters.AddWithValue("@advAtt", DBNull.Value);

                    if (advert.Attachment_Type != null)
                        cmd.Parameters.AddWithValue("@advAttType", advert.Attachment_Type);
                    else
                        cmd.Parameters.AddWithValue("@advAttType", DBNull.Value);

                    cmd.Parameters.AddWithValue("@advName", advert.Name);
                    cmd.Parameters.AddWithValue("@advDescription", advert.Description);
                    cmd.Parameters.AddWithValue("@advUsrId", userId);
                    cmd.Parameters.AddWithValue("@advIsInTwitter", advert.UseTwitter);

                    conn.Open();
                    postTextToWall(advert.Post, advert.Wall.Id);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            advId = dr[0] == null || dr[0] == DBNull.Value ? (int?)null : Convert.ToInt32(dr[0]);

                    }
                }

                query = @"INSERT INTO Campaign (capAdvId, capWalId)
                            VALUES (@idAdv, @idWall)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idAdv", advId ?? 0);
                    cmd.Parameters.AddWithValue("@idWall", advert.Wall.Id);
                    cmd.ExecuteNonQuery();
                }

                return advId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CreateAdvert: " + ex.Message + "!");

                return -1;
            }
            
        }

        private void postTextToWall(string advertText, long? wallid)
        {
            var fb = new FacebookClient(readAccessTokenFromFile());

            ////Get the listing of accounts associated with the user
            dynamic fbAccounts = fb.Get("/me/accounts");

            ////Loop over the accounts looking for the ID that matches your destination ID (Fan Page ID)
            foreach (dynamic account in fbAccounts.data)
            {
                if (wallid == null)
                {
                    dynamic parameters = new ExpandoObject();
                    parameters.message = advertText;

                    fb.PostAsync("/" + account.id + "/feed", parameters);
                }else
                if (wallid.Value == Int64.Parse( account.id))
                {
                    //Then pass your destination ID and target along with FB Post info. You're Done.
                    dynamic parameters = new ExpandoObject();
                    parameters.message = advertText;

                    fb.PostAsync("/" + account.id + "/feed", parameters);
                }
            }
        }

        private void postPictureToWall(string advertText, string filename, byte[] imageArray, long? wallid)
        {
            var fb = new FacebookClient(readAccessTokenFromFile());

            //Get the listing of accounts associated with the user
            dynamic fbAccounts = fb.Get("/me/accounts");
            ////Loop over the accounts looking for the ID that matches your destination ID (Fan Page ID)
            foreach (dynamic account in fbAccounts.data)
            {
                if (wallid == null)
                {
                    dynamic parameters = new ExpandoObject();
                    parameters.message = advertText;
                    parameters.source = new FacebookMediaObject
                    {
                        ContentType = "image/jpeg",
                        FileName = Path.GetFileName(filename)
                    }.SetValue(File.ReadAllBytes(filename));

                    fb.PostAsync("/" + account.id + "/feed", parameters);
                }
                else
                    if (wallid.Value == Int64.Parse(account.id))
                    {
                        dynamic parameters = new ExpandoObject();
                        parameters.message = advertText;
                        parameters.source = new FacebookMediaObject
                        {
                            ContentType = "image/jpeg",
                            FileName = Path.GetFileName(filename)
                        }.SetValue(imageArray);

                        fb.PostAsync("/" + account.id + "/feed", parameters);
                    }
            }
        }

        public void loginToFacebookUsingTestUser()
        {
            CookieCollection cookies = new CookieCollection();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.facebook.com");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);
            //Get the response from the server and save the cookies from the first request..
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cookies = response.Cookies;

            string getUrl = "https://www.facebook.com/login.php?login_attempt=1";
            string postData = String.Format("email={0}&pass={1}", "falvickdeveloper@gmail.com", "ala1234");
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
            getRequest.CookieContainer = new CookieContainer();
            getRequest.CookieContainer.Add(cookies); //recover cookies First request
            getRequest.Method = WebRequestMethods.Http.Post;
            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            getRequest.AllowWriteStreamBuffering = true;
            getRequest.ProtocolVersion = HttpVersion.Version11;
            getRequest.AllowAutoRedirect = true;
            getRequest.ContentType = "application/x-www-form-urlencoded";

            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            getRequest.ContentLength = byteArray.Length;
            Stream newStream = getRequest.GetRequestStream(); //open connection
            newStream.Write(byteArray, 0, byteArray.Length); // Send the data.
            newStream.Close();

            HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                string sourceCode = sr.ReadToEnd();
            }

            string getUrl2 = "https://developers.facebook.com/tools/access_token/";
            HttpWebRequest getRequest2 = (HttpWebRequest)WebRequest.Create(getUrl2);
            getRequest2.CookieContainer = new CookieContainer();
            getRequest2.CookieContainer.Add(cookies); //recover cookies First request
            getRequest2.Method = WebRequestMethods.Http.Post;
            getRequest2.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            getRequest2.AllowWriteStreamBuffering = true;
            getRequest2.ProtocolVersion = HttpVersion.Version11;
            getRequest2.AllowAutoRedirect = true;
            getRequest2.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse getResponse2 = (HttpWebResponse)getRequest2.GetResponse();
            using (StreamReader sr = new StreamReader(getResponse2.GetResponseStream()))
            {
                string sourceCode = sr.ReadToEnd();
            }
        }

        public string readAccessTokenFromFile()
        {
            string filename = @"D:\facebook_access_token.txt";
            string text = System.IO.File.ReadAllText(filename);

            return text;
        }



        public void requestAndParseUserToken()
        {
            
        }

        public AdvertStatus GetAdvertStatus(int advertId)
        {
            return new AdvertStatus();
        }

        public List<Advert> GetAdverts(int userId)
        {
            try
            {
                string query = @"SELECT advId, advName, advAtt, advAttType, advDescription, advUsrId, capWalId, advIsInTwitter, advPost
                            FROM Advert inner join Campaign on capAdvId = advId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Advert> adverts = new List<Advert>();

                        while (dr.Read())
                        {
                            var wallRepo = new WallRepository();
                            var wall = wallRepo.GetWall((dr[6] == null || dr[6] == DBNull.Value) ? 123 : Convert.ToInt64(dr[6]));
                            adverts.Add(new Advert()
                            {
                                Id = (dr[0] == null || dr[0] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr[0]),
                                Name = (dr[1] == null || dr[1] == DBNull.Value) ? null : Convert.ToString(dr[1]),
                                Attachment = (dr[2] == null || dr[2] == DBNull.Value) ? null : (byte[]) dr[2],
                                Attachment_Type = (dr[3] == null || dr[3] == DBNull.Value) ? null : Convert.ToString(dr[3]),
                                Description = (dr[4] == null || dr[4] == DBNull.Value) ? null : Convert.ToString(dr[4]),
                                //UserId = (dr[5] == null || dr[5] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr[5])
                                Post = (dr[8] == null || dr[8] == DBNull.Value) ? null : Convert.ToString(dr[8]),
                                Wall = wall,
                                UseTwitter = (dr[7] == null || dr[7] == DBNull.Value) ? false : Convert.ToInt16(dr[7]) == 1,
                            });
                        }

                        return adverts.Count > 0 ? adverts : null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetAdverts: " + ex.Message);
            }
        }

        public Advert GetAdvert(int advertId)
        {
            try
            {
                string query = @"SELECT advId, advName, advAtt, advAttType, advDescription, advUsrId, capWalId, advIsInTwitter, advPost
                            FROM Advert 
                            inner join Campaign on capAdvId = advId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@advId", advertId);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var wallRepo = new WallRepository();
                            var wall = wallRepo.GetWall((dr[6] == null || dr[6] == DBNull.Value) ? 123 : Convert.ToInt64(dr[6]));

                            return new Advert()
                            {
                                Id = (dr[0] == null || dr[0] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr[0]),
                                Name = (dr[1] == null || dr[1] == DBNull.Value) ? null : Convert.ToString(dr[1]),
                                Attachment = (dr[2] == null || dr[2] == DBNull.Value) ? null : (byte[])dr[2],
                                Attachment_Type = (dr[3] == null || dr[3] == DBNull.Value) ? null : Convert.ToString(dr[3]),
                                Description = (dr[4] == null || dr[4] == DBNull.Value) ? null : Convert.ToString(dr[4]),
                                //UserId = (dr[5] == null || dr[5] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr[5])
                                Post = (dr[8] == null || dr[8] == DBNull.Value) ? null : Convert.ToString(dr[8]),
                                Wall = wall,
                                UseTwitter = (dr[7] == null || dr[7] == DBNull.Value) ? false : Convert.ToInt16(dr[7]) == 1
                            };                        
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetAdvert: " + ex.Message);
            }
        }
    }
}
