using Data_Accesst_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


