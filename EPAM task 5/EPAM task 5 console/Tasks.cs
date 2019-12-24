using System;
using System.Collections.Generic;
using System.Linq;
using EPAM_task_5;

namespace EPAM_task_5_console
{
    class Tasks
    {
        public static void GetCustomersWithProducts()
        {
            IEnumerable<CustomersGoods> list = CustomerGoodsDb.GetCustomerGoods();

            foreach (var customersGoodse in list)
            {
                Console.WriteLine("Customer {0} has {1}", customersGoodse.Customer.Name, customersGoodse.Product.Title);
            }
        }

        public static void GetProductsInCategory(int id)
        {
            IEnumerable<Product> list = GoodsDb.GetProductsByCategoryId(id);
            foreach (var product in list)
            {
                Console.WriteLine("В категории {0} есть товар {1}", CategoryDb.GetCategoryById(id).Name, product.Title);
            }
        }

        public static void GetCustomersInCategory(string name)
        {
            IEnumerable<Customer> customers = CustomerDb.GetAllCustomers();
            IEnumerable<Category> categories = CategoryDb.GetAllCategories();
            
            
            IEnumerable<CustomersGoods> customerGoodsDbs = CustomerGoodsDb.GetCustomerGoods();
            var listOfCategories = categories.Where(p => p.Name == name);

            var needProducts = listOfCategories.Join(customerGoodsDbs,
                c => c.Product.IdGoods, cg => cg.Product.IdGoods, (c, cg) => new {Id = cg.Customer.IdCustomer}  );

            var result = needProducts.Join(customers, arg => arg.Id, c => c.IdCustomer,
                (arg, c) => new {Name = c.Name});

        }

        public static void GetCustomerWithMostCategories()
        {
            var customersHasGoods = CustomerGoodsDb.GetCustomerGoods();
            var categories = CategoryDb.GetAllCategories();

            var subresult = customersHasGoods.Join(
                categories, 
                cg => cg.Product.IdGoods,        
                category => category.Product.IdGoods,
                (cg, category) => new { Category = category, Customer = cg.Customer, Goods = category.Product });

            var preres = subresult.GroupBy(r => r.Customer.Surname).Select(g => new { Name = g.Key, Count = g.Count() });
            var res = preres.OrderByDescending(n => n.Count).First();
            
            Console.WriteLine("Максимально разнообразный {0} - у него товары в {1} категориях", res.Name, res.Count);
            
        }
    }
}