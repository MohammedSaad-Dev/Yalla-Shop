using Microsoft.Data.SqlClient;
using System.Data;
using Data_Access_Layer.DB;

namespace Data_Accesst_Layer
{
    public class Product
    {

        public Product() { }
        public Product(int iD_Product, string name, decimal Price,string Image,int Size )
        {
            this.ID_Product = iD_Product;
            this.Name = name;
            this.Price = Price;
            this.Image = Image;
            this.Size = Size;
        }

        public int ID_Product { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public int Size { get; set; }



        public class ProductData
        {
            public static List<Product> GetAllProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllProducts", conn))
                    {
                        // StoredProcedure -> Stored of DB Ordy

                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }

            public static List<Product> GetBeautyProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new (Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetBeautyProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }


            public static List<Product> GetFragrancesProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetFragrancesProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }

            public static List<Product> GetFurnitureProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetFurnitureProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }


            public static List<Product> GetGroeriesProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetGroeriesProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }

            public static List<Product> GetHomeDecorationProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetHomeDecorationProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }


            public static List<Product> GetJeweleryProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetJeweleryProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }


            public static List<Product> GetMenIsClothingProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetMenIsClothingProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }

            public static List<Product> GetSwomenIsClothingProducts()
            {
                var StudentsList = new List<Product>();

                using (SqlConnection conn = new SqlConnection(Connection_DB._connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SwomenIsClothingProducts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new Product(
                                    reader.GetInt32(reader.GetOrdinal("ID_Product")),
                                    reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image")),
                                    reader.IsDBNull(reader.GetOrdinal("Size")) ? 0 : reader.GetInt32(reader.GetOrdinal("Size"))
                                ));
                            }
                        }
                        conn.Close();
                    }


                    return StudentsList;
                }

            }




            public static List<Product> SearchProducts(string productName)
            {
                List<Product> products = new List<Product>();
                using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "SELECT * FROM Products WHERE Name LIKE @name";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", "%" + productName + "%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new Product(
                           (int)reader["ID_Product"],
                           reader["Name"].ToString(),
                           (decimal)reader["Price"],
                           reader["Image"].ToString(),
                           (int)reader["Size"]
                       ));
                    }
                }
                return products;
            }


            public static Product GetProductByID(int productId)
            {
                Product product = null;
                using (SqlConnection connection = new SqlConnection(Connection_DB._connectionString))
                {
                    string query = "SELECT * FROM Products WHERE ID_Product = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", productId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            ID_Product = (int)reader["ID_Product"],
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"],
                            Image = reader["Image"].ToString(),
                            Size = (int)reader["Size"]
                        };
                    }
                }
                return product;
            }









        }

    }
}
