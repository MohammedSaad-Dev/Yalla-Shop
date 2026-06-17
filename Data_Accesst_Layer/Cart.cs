using Data_Accesst_Layer.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesst_Layer
{
    public class Cart
    {
        public Cart() { }
        public Cart(int Product_ID,int Quantity)
        {
            this.ProductID = Product_ID;
            this.Quantity = Quantity;
        }
       
        public int ProductID { get; set; }
        public int Quantity { get; set; }


        public class CartItemViewModel
        {
            public int CartID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public string Image { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class CartData
        {

            public static string AddToCart(int customerId, int productId, int quantity)
            {
                using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
                {
                    connection.Open();

                    // First, we make sure the product exists
                    string checkProductQuery = "SELECT COUNT(1) FROM Products WHERE ID_Product = @pid";
                    SqlCommand checkProdCmd = new SqlCommand(checkProductQuery, connection);
                    checkProdCmd.Parameters.AddWithValue("@pid", productId);
                    int productExists = (int)checkProdCmd.ExecuteScalar();

                    if (productExists == 0)
                    {
                        return "This product is not in the database, check the ID";
                    }

                    string checkCustomerQuery = "SELECT COUNT(1) FROM Users WHERE ID_User = @cid"; // اتأكد من اسم جدول العملاء عندك
                    SqlCommand checkCustCmd = new SqlCommand(checkCustomerQuery, connection);
                    checkCustCmd.Parameters.AddWithValue("@cid", customerId);
                    int customerExists = (int)checkCustCmd.ExecuteScalar();

                    if (customerExists == 0)
                    {
                        return "This customer is not available, check the ID";
                    }

                    // update or Add

                    string query = @"
            IF EXISTS (SELECT 1 FROM Cart WHERE CustomerID = @cid AND ProductID = @pid)
            BEGIN
                UPDATE Cart SET Quantity = Quantity + @qty WHERE CustomerID = @cid AND ProductID = @pid
            END
            ELSE
            BEGIN
                INSERT INTO Cart (CustomerID, ProductID, Quantity) VALUES (@cid, @pid, @qty)
            END";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cid", customerId);
                    command.Parameters.AddWithValue("@pid", productId);
                    command.Parameters.AddWithValue("@qty", quantity);

                    command.ExecuteNonQuery();
                    return "success";
                }
            }

            public static List<CartItemViewModel> GetCustomerCart(int customerId)
            {
                List<CartItemViewModel> cartList = new List<CartItemViewModel>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_GetCartDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cartList.Add(new CartItemViewModel
                        {
                            CartID = (int)reader["CartID"],
                            ProductName = reader["ProductName"].ToString(),
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"],
                            Image = reader["Image"].ToString(),
                            TotalPrice = (decimal)reader["TotalPrice"]
                        });
                    }
                }
                return cartList;
            }


            public static bool DeleteFromCart(int Customer_ID, int Product_ID)
            {
                using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "DELETE FROM Cart WHERE CustomerID = @cid AND CartID = @pid";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@cid", Customer_ID);
                    command.Parameters.AddWithValue("@pid", Product_ID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; 
                    }
                    catch
                    {
                        return false; 
                    }
                }
            }



            public static bool UpdateQuantity(int Customer_ID, int New_Quantity, int Product_ID)
            {
                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "UPDATE Cart SET Quantity = @qty WHERE CustomerID = @cid AND CartID = @pid"; 
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cid", Customer_ID);
                    cmd.Parameters.AddWithValue("@qty", New_Quantity);
                    cmd.Parameters.AddWithValue("@pid", Product_ID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public static bool ClearCustomerCart(int customerId)
            {
                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "DELETE FROM Cart WHERE CustomerID = @cid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cid", customerId);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }


        }
    }
}
