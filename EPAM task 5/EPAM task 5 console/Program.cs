using System.Collections;
using MySql.Data.MySqlClient;

namespace EPAM_task_5_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks.GetProductsInCategory(2);
            Tasks.GetCustomersInCategory("Электроника");
            Tasks.GetCustomersWithProducts();
            Tasks.GetCustomerWithMostCategories();
        }
    }
}