using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace EPAM_task_5
{
    public class GoodsDb
    {
        private static string connectionString = Connector.connectionString;
        
        public static IEnumerable<Product> GetAllProducts()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string mysqlexpression = "SELECT * FROM Goods";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Product
                            {
                                IdGoods = reader.GetInt32("idGoods"),
                                Title = reader.GetString("title"),
                                Price = reader.GetDecimal("price")
                            };
                        }
                    }
                }
            }
        }

        public static Product GetProductById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = 
                    "SELECT * FROM Goods WHERE idGoods = (@idGoods)";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlParameter idCategory = new MySqlParameter("@idGoods", id);
                    command.Parameters.Add(idCategory);
                
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                             return new Product{
                                IdGoods = reader.GetInt32("idGoods"), 
                                Title = reader.GetString("title"), 
                                Price = reader.GetDecimal("price"),
                                //Category = CategoryDb.GetCategoryById(reader.GetInt32("Category_idCategory"))
                            };
                        }
                    }
                }
            }

            return null;
        }
        
        public static IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = 
                    "SELECT * FROM Goods JOIN Category C2 on Goods.idGoods = C2.Goods_idGoods WHERE C2.idCategory = (@idCategory)";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlParameter idCategory = new MySqlParameter("@idCategory", id);
                    command.Parameters.Add(idCategory);
                
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Product{
                                IdGoods = reader.GetInt32("idGoods"), 
                                Title = reader.GetString("title"), 
                                Price = reader.GetDecimal("price"),
                                //Category = CategoryDb.GetCategoryById(reader.GetInt32("Category_idCategory"))
                            };
                        }
                    }
                }
            }
        }

        public static void AddProduct(Product product)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression =
                    "INSERT INTO Goods VALUES (@title), (@price)";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlParameter title = new MySqlParameter("@title", product.Title);
                    MySqlParameter price = new MySqlParameter("@price", product.Price);

                    command.Parameters.Add(title);
                    command.Parameters.Add(price);
                
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    
}