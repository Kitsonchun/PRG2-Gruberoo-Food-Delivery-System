using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//==========================================================
// Student Number : S10275107B
// Student Name   : Joel Ong Jia Jun
// Partner Name   : Kitson Chun Yi Fan
//==========================================================

namespace Gruberoo_System
{
    public class Customer
    {
        public string EmailAddress { get; set; }
        public string CustomerName { get; set; }

        private List<Order> orders = new List<Order>();

        public Customer(string emailaddress, string customername)
        {
            EmailAddress = emailaddress;
            CustomerName = customername;
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public bool RemoveOrder(Order order)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].OrderId == order.OrderId)
                {
                    orders.RemoveAt(i);
                    return true;
                }
            }

            Console.WriteLine("Order not found");
            return false;
        }

        public void DisplayAllOrders()
        {
            foreach (Order order in orders)
            {
                Console.WriteLine(order);
            }
        }

        public override string ToString()
        {
            return $"";
        }
    }
}
}
