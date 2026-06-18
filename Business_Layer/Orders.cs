using Data_Access_Layer;

using System.Data;


namespace Business_Layer
{
    public class Orders
    {

        public static  string ProcessCheckout(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return "Error: Unknown user";
                }

                int orderId = OrderData.CreateOrderFromCart(userId);

                if (orderId > 0)
                {
                    
                    return orderId.ToString();
                }
                else
                {
                    return "Failed: The basket is empty or an error occurred while creating the order";
                }
            }
            catch (Exception ex)
            {
                return "A technical error occurred: " + ex.Message;
            }
        }

        public static List<object> GetUserOrdersDetailed(int userId)
        {
            DataTable dt = OrderData.GetUserOrdersWithDetails(userId);

            var detailedOrders = dt.AsEnumerable()
               .GroupBy(row => new {
                   OrderId = row["Id"],
                   Date = Convert.ToDateTime(row["OrderDate"]).ToString("yyyy-MM-dd"),
                   Total = row["TotalAmount"],
                   PStatus = row["PaymentStatus"],
                   OStatus = row["OrderStatus"], 
                   Address = row["ShippingAddress"]
               })
               .Select(group => new {
                   orderId = group.Key.OrderId,
                   date = group.Key.Date,
                   totalAmount = group.Key.Total,
                   paymentStatus = group.Key.PStatus,
                   orderStatus = group.Key.OStatus, 
                   shippingAddress = group.Key.Address,
                   products = group.Select(item => new {
                       productName = item["ProductName"],
                       quantity = item["Quantity"],
                       price = item["PriceAtPurchase"]
                   }).ToList()
               }).ToList<object>();

            return detailedOrders;
        }


        public static decimal GetOrderAmount(int orderId)
        {
            DataTable dt = OrderData.GetOrderById(orderId);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToDecimal(dt.Rows[0]["TotalAmount"]);
            }
            return 0;
        }
    }
}

