using Data_Access_Layer.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesst_Layer
{
    public class UserDetailsD
    {

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Method for Insert/Update/Delete (without filling)
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





        public static DataRow GetUserDetailsById(int userId)
            {
                
                string query = "SELECT FirstName, LastName, Email, Phone, Address FROM Users WHERE ID_User = @UserId";

                SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };

                DataTable dt = ExecuteQuery(query, parameters);

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }

            public static bool UpdateUserDetails(int userId, string fName, string lName, string phone, string address)
            {
                string query = @"UPDATE Users 
                         SET FirstName = @FName, LastName = @LName, Phone = @Phone, Address = @Address 
                         WHERE ID_User = @UserId";

                SqlParameter[] parameters = {
            new SqlParameter("@FName", fName),
            new SqlParameter("@LName", lName),
            new SqlParameter("@Phone", phone),
            new SqlParameter("@Address", address),
            new SqlParameter("@UserId", userId)
        };

                return ExecuteNonQuery(query, parameters) > 0;
            }
        }










    
}
