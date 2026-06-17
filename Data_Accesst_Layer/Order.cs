using Data_Accesst_Layer.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data_Accesst_Layer.OrderData;


namespace Data_Accesst_Layer
{
    public class OrderData
    {
        public class Order
        {
            public int Id { get; set; }

            public int UserId { get; set; }

            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string PaymentStatus { get; set; }
            public string OrderStatus { get; set; }

            public virtual ICollection<OrderItem> OrderItems { get; set; }
        }



        public class OrderItem
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal PriceAtPurchase { get; set; }
            public virtual Order Order { get; set; }


        }


        public static int CreateOrderFromCart(int userId)
        {
            using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();


                try
                {
                    // Calculate the total from the cart and enter the main order
                    string insertOrder = @"
                     INSERT INTO Orders (UserId, TotalAmount, OrderDate, PaymentStatus, OrderStatus)
                     SELECT @UserId, SUM(p.Price * c.Quantity), GETDATE(), 'Pending', 'Processing'
                     FROM Cart c JOIN Products p ON c.ProductId = p.ID_Product                           
                     WHERE c.CustomerID = @UserId  
                     GROUP BY c.CustomerID;
                     SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(insertOrder, conn, transaction);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    int orderId = Convert.ToInt32(cmd.ExecuteScalar());


                    // Moving products from the cart to the OrderItems table
                    string insertItems = @"
                    INSERT INTO OrderItems (OrderId, ProductId, Quantity, PriceAtPurchase)
                    SELECT @OrderId, c.ProductId, c.Quantity, p.Price
                    FROM Cart c JOIN Products p ON c.ProductId = p.ID_Product
                    WHERE c.CustomerID = @UserId";

                    cmd.CommandText = insertItems;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Cart WHERE CustomerID = @UserId";
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return orderId; 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex; 
                }
            }
        }



        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters)
           {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    // Open the channel and pull the data
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dt); // Here the table actually gets filled with data
                        connection.Close();

                        }
                        catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return dt;
        }


        public static DataTable GetUserOrdersWithDetails(int userId)
        {
            string query = @"SELECT O.Id, O.OrderDate, O.TotalAmount, O.PaymentStatus, O.OrderStatus, U.Address as ShippingAddress,
                        OI.Quantity, OI.PriceAtPurchase, P.Name as ProductName
                 FROM Orders O
                 JOIN Users U ON O.UserId = U.ID_User
                 JOIN OrderItems OI ON O.Id = OI.OrderId
                 JOIN Products P ON OI.ProductId = P.ID_Product
                 WHERE O.UserId = @UserId
                 ORDER BY O.OrderDate DESC";

            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@UserId", userId)
    };

            return ExecuteQuery(query, parameters);
        }


        public static DataTable GetOrderById(int orderId)
        {
            string query = "SELECT TotalAmount FROM Orders WHERE Id = @OrderId";
            SqlParameter[] parameters = { new SqlParameter("@OrderId", orderId) };
            return ExecuteQuery(query, parameters); 
        }

    }


}
