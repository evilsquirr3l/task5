using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EPAM_task_5
{
    public class CustomerGoodsDb
    {
        private static string connectionString = Connector.connectionString;
        
        public static IEnumerable<CustomersGoods> GetCustomerGoods()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = "SELECT * FROM Customers_has_Goods";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new CustomersGoods{
                                Customer = CustomerDb.GetCustomerById(reader.GetInt32("Customers_idCustomers")),
                                Product = GoodsDb.GetProductById(reader.GetInt32("Goods_idGoods"))
                            };
                        }
                    }
                }
            }
        }
    }
}