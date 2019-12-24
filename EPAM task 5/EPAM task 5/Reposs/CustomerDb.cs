using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EPAM_task_5
{
    public class CustomerDb
    {
        private static string connectionString = Connector.connectionString;
        
        public static Customer GetCustomerById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = "SELECT * FROM Customers WHERE idCustomers = (@idCustomer)";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlParameter idCategory = new MySqlParameter("@idCustomer", id);
                    command.Parameters.Add(idCategory);
                
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return new Customer(){
                                IdCustomer = reader.GetInt32("idCustomers"),
                                Name = reader.GetString("name"),
                                Surname = reader.GetString("surname")
                            };
                        }
                    }
                }
            }

            return null;
        }
        
        public static IEnumerable<Customer> GetAllCustomers()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string mysqlexpression = "SELECT * FROM Customers";

                using (MySqlCommand command = new MySqlCommand(mysqlexpression, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Customer(){
                                IdCustomer = reader.GetInt32("idCustomers"),
                                Name = reader.GetString("name"),
                                Surname = reader.GetString("surname")
                            };
                        }
                    }
                }
            }
        }
    }
}