using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;

namespace Business_Layer
{
    public class Products
    {


        public static List<Product> GetAllProducts()
        {
            return Product.ProductData.GetAllProducts();


        }

        public static List<Product> GetBeautyProducts()
        {
            return Product.ProductData.GetBeautyProducts();


        }


        public static List<Product> GetFragrancesProducts()
        {
            return Product.ProductData.GetFragrancesProducts();


        }
        public static List<Product> GetFurnitureProducts()
        {
            return Product.ProductData.GetFurnitureProducts();


        }
        public static List<Product> GetGroeriesProducts()
        {
            return Product.ProductData.GetGroeriesProducts();


        }
        public static List<Product> GetHomeDecorationProducts()
        {
            return Product.ProductData.GetHomeDecorationProducts();

        }


        public static List<Product> GetJeweleryProducts()
        {
            return Product.ProductData.GetJeweleryProducts();



        }


        public static List<Product> GetMenIsClothingProducts()
        {
            return Product.ProductData.GetMenIsClothingProducts();



        }


        public static List<Product> GetSwomenIsClothingProducts()
        {
            return Product.ProductData.GetSwomenIsClothingProducts();



        }

        public static List<Product> GetSearchQuery(string name)
        {
            return Product.ProductData.SearchProducts(name);
        }




        public static Product GetDetails(int id)
        {
            return Product.ProductData.GetProductByID(id);
        }

    }
   


}
