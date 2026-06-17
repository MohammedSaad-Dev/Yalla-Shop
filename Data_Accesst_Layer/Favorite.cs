using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Data_Accesst_Layer.DB;



namespace Data_Access_Layer
{
    public class FavoriteData
    {
        public class FavoriteRequest
        {
            public int productId { get; set; }
        }
        public static DataTable GetCustomerFavorites(int customerId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
            {
                
                string query = @"
            SELECT Products.ID_Product, Products.Name, Products.Price, Products.Size, Products.Image
            FROM Favorites
            INNER JOIN Products ON Favorites.ID_Product = Products.ID_Product
            WHERE Favorites.ID_User = @cid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cid", customerId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

    
        public static bool AddToFavorites(int customerId, int productId)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
            {
                string query = "INSERT INTO Favorites (ID_User, ID_Product) VALUES (@cid, @pid)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cid", customerId);
                    command.Parameters.AddWithValue("@pid", productId);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static bool RemoveFromFavorites(int customerId, int productId)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
            {
                string query = "DELETE FROM Favorites WHERE ID_User = @cid AND ID_Product = @pid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cid", customerId);
                    command.Parameters.AddWithValue("@pid", productId);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch { return false; }
                }
            }
            return rowsAffected > 0;
        }
    }
}