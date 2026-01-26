using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gruberoo_System;

// Feature 2 , 4 , 6 ,8 //

List<Customer> CUSTOMER = new List<Customer>();
List<Order> ORDER = new List<Order>();

void LoadCustomerfile()
{
	string[] Cus = File.ReadAllLines("customers.csv");

	foreach (string c in Cus.Skip(1)) 
	{
		var parts = c.Split(',');
		string customername = parts[0];
		string emailaddress = parts[1];

		Customer customer = new Customer(customername, emailaddress);
		CUSTOMER.Add(customer);


	}
}

void LoadOrderFile()
{
	string[] ord = File.ReadAllLines("orders.csv");

	foreach (string o in ord.Skip(1))
	{
		var parts = o.Split(',');
        int orderId = int.Parse(parts[0].Trim());
        string customerEmail = parts[1].Trim();
        string restaurantId = parts[2].Trim();
        string deliveryDate = parts[3].Trim();
        string deliveryTime = parts[4].Trim();
        string deliveryAddress = parts[5].Trim();
        string createdDateTime = parts[6].Trim();
        decimal totalAmount = decimal.Parse(parts[7].Trim());
        string status = parts[8].Trim();
        string items = parts[9].Trim();

        Order order = new Order(orderId, customerEmail, restaurantId, deliveryDate,
                                       deliveryTime, deliveryAddress, createdDateTime,
                                       totalAmount, status, items);

        ORDER.Add(order);
    }
}








