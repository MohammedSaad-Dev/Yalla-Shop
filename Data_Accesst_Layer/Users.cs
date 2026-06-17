using Data_Accesst_Layer.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data_Accesst_Layer
{


    public class User
    {
        public User (int ID_User, string FirstName,string LastName, string Email,string Password,string Phone, string address)
        {
            this.ID_User = ID_User;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email; 
            this.Phone = Phone;
            this.Address=address;
            this.Password = Password;



        }
        public User() { }

        public int ID_User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Password { get; set; }



        

        public class UserData
        {

            public static bool IsEmailExist(string email)
            {
                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "SELECT COUNT(1) FROM Users WHERE Email = @email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }

            public static User SignUp(string FirstName,string LastName, string email, string password)
            {
                if (IsEmailExist(email))
                {
                    return null; 
                }

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "INSERT INTO Users (FirstName, LastName, Email, Password) OUTPUT INSERTED.ID_User VALUES (@firstName, @lastName, @email, @pass )";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@firstName", FirstName);
                    cmd.Parameters.AddWithValue("@lastName", LastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", password);
               

                    conn.Open();
                    int newId = (int)cmd.ExecuteScalar();

                    return new User { ID_User = newId, FirstName = FirstName, LastName = LastName, Email = email };
                }
            }

           



            public static User Login(string email, string password)
            {
                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    
                    string query = "SELECT ID_User,FirstName, LastName, Email FROM Users WHERE Email = @email AND Password = @pass";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new User
                        {
                            ID_User = (int)reader["ID_User"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
                return null;
            
                }







            public static bool UpdateUserContactInfo(int userId, string newAddress, string newPhone)
            {
                using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = @"UPDATE Users 
                         SET Address = @Address, 
                             Phone = @Phone 
                         WHERE ID_User = @UserId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Address", newAddress);
                    command.Parameters.AddWithValue("@Phone", newPhone);
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;

                }
            }





        }
    }
}
