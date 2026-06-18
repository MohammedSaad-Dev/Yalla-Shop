using Data_Access_Layer; 
using System.Data;

namespace Business_Layer
{
    public class Favorites
    {

        public static DataTable GetAllFavorites(int customerId)
        {
            return FavoriteData.GetCustomerFavorites(customerId);
        }

        public static bool AddToFavorites(int customerId, int productId)
        {
        

            return FavoriteData.AddToFavorites(customerId, productId);
        }

        public static bool RemoveFromFavorites(int customerId, int productId)
        {
            return FavoriteData.RemoveFromFavorites(customerId, productId);
        }
       
    }
}
