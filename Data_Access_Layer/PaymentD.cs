using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.DB;
namespace Data_Accesst_Layer
{
    public class PaymentD
    {

        
          
            public  static DataTable GetData(string query)
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
                return dt;
            }

        // The method that prepares the payment data
        public static DataTable GetPaymentData(int orderId, int userId)
            {
                string query = $@"SELECT U.FirstName,U.LastName, U.Email, U.Phone, U.Address, O.TotalAmount 
                          FROM Users U 
                          JOIN Orders O ON U.ID_User = O.UserId 
                          WHERE O.Id = {orderId} AND O.UserId = {userId}";

                return GetData(query); 
            }
        public  static DataTable GetOrderItems(int orderId)
        {
            string query = $@"SELECT P.Name, OI.Quantity, OI.PriceAtPurchase 
                      FROM OrderItems OI
                      JOIN Products P ON OI.ProductId = P.ID_Product
                      WHERE OI.OrderId = {orderId}";

            return GetData(query);
        }
        public static int ExecuteNonQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    //  (Update/Insert/Delete) 
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static bool UpdateOrderStatus(int orderId, string paymentStatus)
        {

            // Determine the order status (OrderStatus) based on the payment status coming from your invoices
            string orderStatus;
            switch (paymentStatus.ToLower())
            {
                case "paid":
                    orderStatus = "Confirmed"; 
                    break;
                case "failed":
                    orderStatus = "Payment Failed"; 
                    break;
                case "cancelled":
                    orderStatus = "Cancelled";
                    break;
                default:
                    orderStatus = "Pending";
                    break;
            }

            string query = $@"UPDATE Orders 
                      SET PaymentStatus = '{paymentStatus}', 
                          OrderStatus = '{orderStatus}' 
                      WHERE Id = {orderId}"; 

            return ExecuteNonQuery(query) > 0;
        }








    }
}
