using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rage.Addservice.Domain.Repositories;
using Rage.Addservice.Domain.Model;
using System.Data.SqlClient;
using System.Drawing;

namespace Rage.Addservice.Persistence.Repositories
{
    public class WallRepository : IWallRepository
    {
        private string connectionString = "Server=(local);Database=SuperServer; User Id=pk; Password=pk;";

        public List<Wall> GetWalls()
        {
            try
            {
                string query = @"SELECT walId, walName, walDescription, walImage
                            FROM Wall";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Wall> walls = new List<Wall>();
                        var image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.default_wall_image));
                        while (dr.Read())
                        {
                            walls.Add(new Wall()
                            {
                                Id = (dr[0] == null || dr[0] == DBNull.Value) ? (long?)null : Convert.ToInt64(dr[0]),
                                Name = (dr[1] == null || dr[1] == DBNull.Value) ? null : Convert.ToString(dr[1]),
                                Description = (dr[2] == null || dr[2] == DBNull.Value) ? null : Convert.ToString(dr[2]),
                                Image = (dr[3] == null || dr[3] == DBNull.Value) ? image : (Byte[])dr[3]
                            });
                        }

                        return walls.Count > 0 ? walls : null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetWalls: " + ex.Message);
            }
        }

        public Wall GetWall(long walId)
        {
            try
            {
                string query = @"SELECT walId, walName, walDescription, walImage
                            FROM Wall
                            WHERE walId = @walId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@walId", walId);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var image = ImageToByteConverter.ConvertToByteArray(new Bitmap(Rage.Addservice.Persistence.Properties.Resources.default_wall_image));
                            return new Wall()
                            {
                                Id = (dr[0] == null || dr[0] == DBNull.Value) ? (long?)null : Convert.ToInt64(dr[0]),
                                Name = (dr[1] == null || dr[1] == DBNull.Value) ? null : Convert.ToString(dr[1]),
                                Description = (dr[2] == null || dr[2] == DBNull.Value) ? null : Convert.ToString(dr[2]),
                                Image = (dr[3] == null || dr[3] == DBNull.Value) ? image : (Byte[]) dr[3]
                            };
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetWall: " + ex.Message);
            }
        }

        public void InsertImage(long walId, Byte[] image)
        {
            try
            {
                string query = "UPDATE Wall SET walImage = @image WHERE walId = @walId ";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@image", image);
                    cmd.Parameters.AddWithValue("@walId", walId);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("InsertImage error: " +e.Message);
            }
        }  
    }
}
