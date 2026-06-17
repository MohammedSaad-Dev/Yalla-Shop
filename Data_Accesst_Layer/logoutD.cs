using Data_Access_Layer.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Data_Access_Layer
{
    public class logoutD
    {
        public static bool IsTokenInBlacklist(string token)
        {
            int count = 0;
         
            string query = "SELECT COUNT(1) FROM BlacklistedTokens WHERE Token = @Token";

            using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", token);
                    connection.Open();
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return count > 0;
        }


        public static bool AddTokenToBlacklist(string token)
        {
            string query = "INSERT INTO BlacklistedTokens (Token, ExpiryDate) VALUES (@Token, @Expiry)";

            SqlParameter[] parameters = {
        new SqlParameter("@Token", token),
        new SqlParameter("@Expiry", DateTime.Now.AddDays(1))
    };

            int rows = ExecuteNonQuery(query, parameters);
            return rows > 0;
        }


        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}