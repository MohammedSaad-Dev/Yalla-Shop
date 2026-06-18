using Data_Access_Layer;

namespace Business_Layer
{
    public class Carts
    {


   
        
        public static string AddToCart(int customerId, int productId, int quantity)
        {
            return Cart.CartData.AddToCart(customerId, productId, quantity);
        }

        public static List<Cart.CartItemViewModel> GetCustomerCart(int customerId)
        {
            return Cart.CartData.GetCustomerCart(customerId);
        }

        public static bool DeleteFromCart(int customerId, int productId)
        {
            return Cart.CartData.DeleteFromCart(customerId, productId);
        }

        public static bool UpdateQuantity(int customerId,  int newQuantity,int productId)
        {
            return Cart.CartData.UpdateQuantity(customerId, newQuantity,productId);
        }

        public static bool ClearCustomerCart(int customerId)
        {
            return Cart.CartData.ClearCustomerCart(customerId);
        }

    }

}


