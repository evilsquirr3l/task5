using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EPAM_task_5
{
    class ProductDb
    {
        private static string connectionString = Connector.connectionString;
        
        public static IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = "SELECT * FROM Goods JOIN Category C2 on Goods.idGoods = C2.Goods_idGoods WHERE C2.Category_idCategory = (@idCategory)";

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
                            };
                        }
                    }
                }
            }
        }
    }
}