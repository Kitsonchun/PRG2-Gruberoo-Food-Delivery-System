using Gruberoo_System;


// Doing features 1, 4, 6 & 8 //
//==========================================================
// Student Number : S10275142J
// Student Name   : Kitson Chun Yi Fan
// Partner Name   : Joel Ong Jia Jun 
//==========================================================

List<Restaurant> restaurants = new List<Restaurant>();
Dictionary<string, Menu> restaurantMenus = new Dictionary<string, Menu>();

// Feature 1 //
void LoadRestaurantsfile()
{
    string[] ResLines = File.ReadAllLines("restaurant.csv");

    foreach (string res in ResLines.Skip(1))
    {
        var parts = res.Split(',');
        string restaurantId = parts[0];
        string restaurantName = parts[1];
        string restaurantEmail = parts[2];

        Restaurant restaurant = new Restaurant(restaurantId, restaurantName, restaurantEmail);
        restaurants.Add(restaurant);


    }
}

void LoadFooditemsfile()
{
    string[] FoodLines = File.ReadAllLines("fooditems.csv");
    List<FoodItem> foodItems = new List<FoodItem>();

    foreach (string food in FoodLines.Skip(1))
    {
        var parts = food.Split(',');
        string restaurantId = parts[0];
        string itemName = parts[1];
        string itemDesc = parts[2];
        double itemPrice = Convert.ToDouble(parts[3]);
        string customise = parts[4];

        FoodItem foodItem = new FoodItem(itemName, itemDesc, itemPrice, customise);

        foreach (Restaurant restaurant in restaurants)
        {
            if (!restaurantMenus.ContainsKey(restaurantId))
            {
                Menu menu = new Menu("Menu1", "Main Menu");
                restaurant.AddMenu(menu);
                restaurantMenus[restaurantId] = menu;
            }

            restaurantMenus[restaurantId].AddFoodItem(foodItem);
            break;

        }
    }
}


// Feature 4 //
void ListAllOrder(List<Order> orders)
{
    Console.WriteLine("All Orders");
    Console.WriteLine("==========");

    foreach (Order order in orders)
    {
        order.DisplayOrderdFoodItems();
    }

    if (orders.Count == 0)
    {
        Console.WriteLine("No orders found.");
    }
}

// Feature 6 //
void ProcessOrder(Queue<Order> orderQueue, Stack<Order> refundStack)
{
    Console.WriteLine("Process Order");
    Console.WriteLine("=============");
    Console.Write("Enter Restaurant ID: ");
    string restaurantId = Convert.ToString(Console.ReadLine());

    int i = 1;

    Queue<Order> newQueue = new Queue<Order>();

    foreach (Order order in orderQueue)
    {
        if (order.Restaurant.RestaurantID == restaurantId)
        {
            newQueue.Enqueue(order);

            Console.WriteLine($"Order {order.OrderId}");
            Console.WriteLine($"Customer: {order.Customer.CustomerName}");
            Console.WriteLine("Ordered Items:");

            foreach (OrderedFoodItem item in order.OrderedFoodItems)
            {
                Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
                i++;
            }

            Console.WriteLine($"Delivery date/time: {order.DeliveryDateTime:DD/MM/yyyy hh:mm}");
            Console.WriteLine($"Total Amount: ${order.CalculateOrderTotal()}");
            Console.WriteLine($"Order Status: {order.OrderStatus}");

            Console.Write("[C]onfirm / [R]eject / [S]kip / [D]eliver: ");
            string choice = Convert.ToString(Console.ReadLine()).ToUpper();

            if (choice == "C")
            {
                if (order.OrderStatus == "Pending")
                {
                    order.OrderStatus = "Preparing";
                    Console.WriteLine(order.ToString());
                }

                else
                {
                    Console.WriteLine("Order cannot be confirmed.");
                }
            }

            else if (choice == "R")
            {
                if (order.OrderStatus == "Pending")
                {
                    order.OrderStatus = "Rejected";
                    refundStack.Push(order);
                    orderQueue.Dequeue();
                    Console.WriteLine(order.ToString());
                }

                else
                {
                    Console.WriteLine("Order cannot be rejected.");
                }
            }

            else if (choice == "S")
            {
                if (order.OrderStatus == "Cancelled")
                {
                    orderQueue.Dequeue();
                    Console.WriteLine(order.ToString());
                }

                else
                {
                    Console.WriteLine("Order cannot be skipped.");
                }
            }

            else if (choice == "D")
            {
                if (order.OrderStatus == "Pending")
                {
                    string newstatus = order.OrderStatus = "Delivered";
                    orderQueue.Dequeue();
                    Console.WriteLine(order.ToString());
                }
            }
        }
    }
}

// Feature 8 //
void DeletingExistingOrder(Queue<Order> orderQueue, Stack<Order> refundStack)
{
    Console.WriteLine("Delete Order");
    Console.WriteLine("=============");
    Console.Write("Enter Customer Email: ");
    string Email = Convert.ToString(Console.ReadLine());

    Console.WriteLine("Pending Orders:");
    foreach (Order order in orderQueue)
    {
        if (order.Customer.EmailAddress == Email)
        {
            if (order.OrderStatus == "Pending")
            {
                Console.WriteLine(order.OrderId);
                Console.Write("Enter Order ID: ");
                Console.WriteLine("Ordered Items:");
                int i = 1;
                foreach (OrderedFoodItem item in order.OrderedFoodItems)
                {
                    Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
                    i++;
                }

                Console.WriteLine($"Delivery date/time: {order.DeliveryDateTime:DD/MM/yyyy hh:mm}");
                Console.WriteLine($"Total Amount: ${order.CalculateOrderTotal}");
                Console.WriteLine($"Order Status: {order.OrderStatus}");
                Console.Write("Confirm deletaion? [Y/N]: ");
                string choice = Convert.ToString(Console.ReadLine()).ToUpper();
                if (choice == "Y")
                {
                    order.OrderStatus = "Cancelled";
                    Console.WriteLine($"Order {order.OrderId} cancelled. Refund of {order.OrderPaid} processed.");
                }
                else
                {
                    Console.WriteLine("Order deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("No Orders found.");
            }
        }
    }
}


// Doing feature 2 , 3 , 5 ,7 //
// Advanced Feature B //
//==========================================================
// Student Number : S10275107B
// Student Name   : Joel Ong Jia Jun
// Partner Name   : Kitson Chun Yi Fan
//==========================================================

List<Customer> CUSTOMER = new List<Customer>();
List<Order> ORDER = new List<Order>();
List<Restaurant> RESTAURANTS = new List<Restaurant>();

// Feature 2 //
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
        restaurant.order.Add(order);
    }
}

// Feature 3 //



void LoadSystem()
{
    Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
    LoadRestaurantsfile();
    LoadFooditemsfile();
    LoadCustomerfile();
    LoadOrderFile();
    Console.WriteLine("15 restaurants loaded!");
    Console.WriteLine("51 food items loaded!");
    Console.WriteLine("20 customers loaded!");
    Console.WriteLine("35 orders loaded!");
}

void DisplaySystem()
{
    Console.WriteLine("===== Gruberoo Food Delivery System =====");
    Console.WriteLine("1. Lista all restaurants and menu items");
    Console.WriteLine("2. List all orders");
    Console.WriteLine("3. Create a new order");
    Console.WriteLine("4. Process an order");
    Console.WriteLine("5. Modify an existing order");
    Console.WriteLine("6. Delete an existing order");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");
    string choice = Convert.ToString(Console.ReadLine());

    if (choice == "1")
    {

    }
}
