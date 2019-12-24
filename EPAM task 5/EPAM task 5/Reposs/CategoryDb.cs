using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EPAM_task_5
{
    public class CategoryDb
    {
        private static string connectionString = Connector.connectionString;
        public static Category GetCategoryById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = "SELECT * FROM Category WHERE idCategory = (@idCategory)";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlParameter idCategory = new MySqlParameter("@idCategory", id);
                    command.Parameters.Add(idCategory);
                
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new Category(){
                                IdCategory = reader.GetInt32("idCategory"),
                                Name = reader.GetString("category")
                            };
                        }
                    }
                }
            }

            return null;
        }
        
        public static IEnumerable<Category> GetAllCategories()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = "SELECT * FROM Category";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Category(){
                                IdCategory = reader.GetInt32("idCategory"),
                                Name = reader.GetString("category"), 
                                Product = GoodsDb.GetProductById(reader.GetInt32("Goods_idGoods"))
                            };
                        }
                    }
                }
            }
        }
    }
}