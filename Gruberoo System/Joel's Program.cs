using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gruberoo_System;

// Feature 2 , 4 , 6 ,8 //

List<Customer> CUSTOMER = new List<Customer>();
List<Order> ORDER = new List<Order>();
List<Restaurant> RESTAURANTS = new List<Restaurant>();

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

        DateTime deliveryDateTime = DateTime.Parse(
            parts[3].Trim() + " " + parts[4].Trim()
        );

        string deliveryAddress = parts[5].Trim();
        double totalAmount = double.Parse(parts[7].Trim());
        string status = parts[8].Trim();

        // 🔍 find customer object
        Customer customer = CUSTOMER
            .FirstOrDefault(c => c.EmailAddress == customerEmail);

        // 🔍 find restaurant object
        Restaurant restaurant = RESTAURANTS
            .FirstOrDefault(r => r.RestaurantID == restaurantId);

        if (customer == null || restaurant == null)
            continue;

        Order order = new Order(
            orderId,
            customer,
            restaurant,
            deliveryDateTime,
            deliveryAddress,
            totalAmount,
            status
        );

        ORDER.Add(order);
        customer.AddOrder(order);
        restaurant.(order);
    }
}








