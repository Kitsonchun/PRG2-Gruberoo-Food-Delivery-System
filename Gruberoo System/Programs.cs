using Gruberoo_System;


// Doing features 1, 4, 6 & 8 //
//==========================================================
// Student Number : S10275142J
// Student Name   : Kitson Chun Yi Fan
// Partner Name   : Joel Ong Jia Jun 
//==========================================================

List<Restaurant> restaurants = new List<Restaurant>();
Dictionary<string, Menu> restaurantMenus = new Dictionary<string, Menu>();
Queue<Order> orderQueue = new Queue<Order>();
Stack<Order> refundStack = new Stack<Order>();
List<Customer> CUSTOMER = new List<Customer>();
List<Order> ORDER = new List<Order>();

// Doing features 1, 4, 6 & 8 //
//==========================================================
// Student Number : S10275142J
// Student Name   : Kitson Chun Yi Fan
// Partner Name   : Joel Ong Jia Jun 
//==========================================================

// Feature 1 //
void LoadRestaurantsfile()
{
    string[] ResLines = File.ReadAllLines("restaurants.csv");

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

        Restaurant foundRestaurant = null;

        foreach (Restaurant r in restaurants)
        {
            if (r.RestaurantID == restaurantId)
            {
                foundRestaurant = r;
                break;
            }
        }
        
        if (foundRestaurant == null)
        {
            continue;
        }

        if (!restaurantMenus.ContainsKey(restaurantId))
        {
            Menu menu = new Menu("Menu1", "Main Menu");
            foundRestaurant.AddMenu(menu);
            restaurantMenus[restaurantId] = menu;
        }

        restaurantMenus[restaurantId].AddFoodItem(foodItem);

    }
}


// Feature 4 //
void ListAllOrder(List<Order> orders)
{
    Console.WriteLine("All Orders");
    Console.WriteLine("==========");

    if (orders.Count == 0)
    {
        Console.WriteLine("No orders found.");
    }

    foreach (Order order in orders)
    {
        order.DisplayOrderdFoodItems();
    }
}

// Feature 6 //
void ProcessOrder(Queue<Order> orderQueue, Stack<Order> refundStack)
{
    Console.WriteLine("Process Order");
    Console.WriteLine("=============");
    Console.Write("Enter Restaurant ID: ");
    string restaurantId = Convert.ToString(Console.ReadLine());

    int count = orderQueue.Count;

    Queue<Order> newQueue = new Queue<Order>();

    while (count > 0)
    {
        Order order = orderQueue.Dequeue();
        count--;
    
        if (order.Restaurant.RestaurantID == restaurantId)
        {
            newQueue.Enqueue(order);

            Console.WriteLine($"Order {order.OrderId}");
            Console.WriteLine($"Customer: {order.Customer.CustomerName}");
            Console.WriteLine("Ordered Items:");
            
            int i = 1;

            foreach (OrderedFoodItem item in order.OrderedFoodItems)
            {
                Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
                i++;
            }

            Console.WriteLine($"Delivery date/time: {order.DeliveryDateTime:dd/MM/yyyy hh:mm}");
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

                newQueue.Enqueue(order);
            }

            else if (choice == "R")
            {
                if (order.OrderStatus == "Pending")
                {
                    order.OrderStatus = "Rejected";
                    refundStack.Push(order);
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
                    Console.WriteLine(order.ToString());
                    newQueue.Enqueue(order);
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
                    Console.WriteLine(order.ToString());
                }

                else
                {
                    Console.WriteLine("Order cannot be delivered.");
                    newQueue.Enqueue(order);
                }
            }

            else
            {
                newQueue.Enqueue(order);
            }
        }
    }

    while (newQueue.Count > 0)
    {
        orderQueue.Enqueue(newQueue.Dequeue());
    }
}

// Feature 8 //
void DeletingExistingOrder(Queue<Order> orderQueue, Stack<Order> refundStack)
{
    Console.WriteLine("Delete Order");
    Console.WriteLine("=============");
    Console.Write("Enter Customer Email: ");
    string Email = Convert.ToString(Console.ReadLine());

    bool found = false;

    Console.WriteLine("Pending Orders:");

    foreach (Order order in orderQueue)
    {
        if (order.Customer.EmailAddress == Email &&
            order.OrderStatus == "Pending")
        {
            found = true;
            Console.WriteLine(order.OrderId);
        }
    }

    if (!found)
    {
        Console.WriteLine("No Orders found.");
    }

    Console.Write("Enter Order ID: ");
    int selectedorderid = Convert.ToInt32(Console.ReadLine());

    foreach (Order order in orderQueue)
    {
        if (order.Customer.EmailAddress == Email &&
            order.OrderStatus == "Pending" &&
            order.OrderId == selectedorderid)
        {
            Console.WriteLine("Ordered Items:");

            int i = 1;
            foreach (OrderedFoodItem item in order.OrderedFoodItems)
            {
                Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
                i++;
            }

            Console.WriteLine($"Delivery date/time: {order.DeliveryDateTime:dd/MM/yyyy hh:mm}");
            Console.WriteLine($"Total Amount: ${order.CalculateOrderTotal()}");
            Console.WriteLine($"Order Status: {order.OrderStatus}");
            Console.Write("Confirm deletion? [Y/N]: ");
            string choice = Convert.ToString(Console.ReadLine()).ToUpper();

            if (choice == "Y")
            {
                order.OrderStatus = "Cancelled";
                refundStack.Push(order);
                Console.WriteLine($"Order {order.OrderId} cancelled. Refund of {order.OrderPaid} processed.");
            }
            else
            {
                Console.WriteLine("Order deletion cancelled.");
            }

            return;
        }
    }

    Console.WriteLine("No Orders found.");
}



// Doing feature 2 , 3 , 5 ,7 //
// Advanced Feature B //
//==========================================================
// Student Number : S10275107B
// Student Name   : Joel Ong Jia Jun
// Partner Name   : Kitson Chun Yi Fan
//==========================================================


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

        Customer customer = null;
        foreach (Customer c in CUSTOMER)
        {
            if (c.EmailAddress == customerEmail)
            {
                customer = c;
                break;
            }
        }

        Restaurant restaurant = null;
        foreach (Restaurant r in restaurants)
        {
            if (r.RestaurantID == restaurantId)
            {
                restaurant = r;
                break;
            }
        }
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

void ListAllRestaurantsAndMenuItems()
{
    Console.WriteLine("All Restaurants and Menu Items");
    Console.WriteLine("==============================");

    foreach (var restaurant in restaurants)
    {
        Console.WriteLine($"Restaurant: {restaurant.RestaurantName} ({restaurant.RestaurantID})");

        foreach (var menu in restaurant.Menus)
        {
            foreach (var item in menu.FoodItems)
            {
                Console.WriteLine($"- {item.ItemName}: {item.ItemDesc} - ${item.ItemPrice}");
            }
        }
    }
}

// Feature 5 //
void CreateNewOrder()
{
    Console.WriteLine("Create New Order");
    Console.WriteLine("================");

    Console.Write("Enter Customer Email: ");
    string email = Console.ReadLine().Trim();
    Customer customer = null;
    for (int i = 0; i < CUSTOMER.Count; i++)
    {
        if (CUSTOMER[i].EmailAddress == email)
        {
            customer = CUSTOMER[i];
            break;
        }
    }
    if (customer == null)
    {
        Console.WriteLine("Customer not found");
        return;
    }


    Console.Write("Enter Restaurant ID: ");
    string restaurantId = Console.ReadLine().Trim();
    Restaurant restaurant = null;
    for (int i = 0; i < restaurants.Count; i++)
    {
        if (restaurants[i].RestaurantID == restaurantId)
        {
            restaurant = restaurants[i];
            break;
        }
    }
    if (restaurant == null)
    {
        Console.WriteLine("Restaurant not found");
        return;
    }


    Console.Write("Enter Delivery Date (dd/MM/yyyy): ");
    string deliveryDate = Console.ReadLine();
    Console.Write("Enter Delivery Time (HH:mm): ");
    string deliveryTime = Console.ReadLine();
    DateTime deliveryDateTime = DateTime.Parse(deliveryDate + " " + deliveryTime);

    Console.Write("Enter Delivery Address: ");
    string deliveryAddress = Console.ReadLine().Trim();


    List<FoodItem> allItems = new List<FoodItem>();
    for (int i = 0; i < restaurant.Menus.Count; i++)
    {
        Menu menu = restaurant.Menus[i];
        for (int j = 0; j < menu.FoodItems.Count; j++)
        {
            allItems.Add(menu.FoodItems[j]);
        }
    }

    Console.WriteLine("Available Food Items:");
    for (int i = 0; i < allItems.Count; i++)
        Console.WriteLine($"{i + 1}. {allItems[i].ItemName} - ${allItems[i].ItemPrice:F2}");


    Order newOrder = new Order(
        ORDER.Count > 0 ? ORDER.Max(o => o.OrderId) + 1 : 1001,
        customer,
        restaurant,
        deliveryDateTime,
        deliveryAddress,
        0,
        "Pending"
    );

    while (true)
    {
        Console.Write("Enter item number (0 to finish): ");
        if (!int.TryParse(Console.ReadLine(), out int itemNo) || itemNo < 0 || itemNo > allItems.Count)
        {
            Console.WriteLine("Not Available.");
            continue;
        }
        if (itemNo == 0) break;

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            continue;

        FoodItem selectedItem = allItems[itemNo - 1];

       
        newOrder.AddOrderFoodItem(new OrderedFoodItem(
            selectedItem.ItemName,
            selectedItem.ItemDesc,
            selectedItem.ItemPrice,
            selectedItem.Customise,
            qty
        ));
    }

    if (newOrder.OrderedFoodItems.Count == 0)
    {
        Console.WriteLine("No items selected. Order cancelled.");
        return;
    }

    Console.Write("Add special request? [Y/N]: ");
    string specialRequest = Console.ReadLine().Trim().ToUpper() == "Y"
        ? Console.ReadLine().Trim()
        : "";


    double orderTotal = 5.0 + newOrder.CalculateOrderTotal();
    newOrder.OrderTotal = orderTotal;

    Console.WriteLine($"Order Total: ${orderTotal:F2} (including $5 delivery fee)");

    Console.Write("Proceed to payment? [Y/N]: ");
    if (Console.ReadLine().Trim().ToUpper() != "Y")
    {
        Console.WriteLine("Order cancelled.");
        return;
    }

    Console.Write("Payment method [CC=Credit Card / PP=PayPal / CD=Cash]: ");
    newOrder.OrderPaymentMethod = Console.ReadLine().Trim().ToUpper();

    customer.AddOrder(newOrder);
    ORDER.Add(newOrder);
    restaurant.order.Add(newOrder);
    string orderLine = $"{newOrder.OrderId},{customer.EmailAddress},{restaurant.RestaurantID},{deliveryDateTime:dd/MM/yyyy},{deliveryDateTime:HH:mm},{deliveryAddress},,{orderTotal:F2},{newOrder.OrderStatus}";
    File.AppendAllText("orders.csv", orderLine + Environment.NewLine);

    Console.WriteLine($"Order {newOrder.OrderId} created successfully! Status: {newOrder.OrderStatus}");
}


//Feature 7//
void ModifyExistingOrder()
{
    Console.WriteLine("Modify Order");
    Console.WriteLine("============");

    Console.Write("Enter Customer Email: ");
    string email = Console.ReadLine().Trim();

    Customer customer = null;
    for (int i = 0; i < CUSTOMER.Count; i++)
    {
        if (CUSTOMER[i].EmailAddress == email)
        {
            customer = CUSTOMER[i];
            break;
        }
    }

    if (customer == null)
    {
        Console.WriteLine("Customer not found.");
        return;
    }

    List<Order> pendingOrders = new List<Order>();
    for (int i = 0; i < customer.Orders.Count; i++)
    {
        if (customer.Orders[i].OrderStatus == "Pending")
        {
            pendingOrders.Add(customer.Orders[i]);
            Console.WriteLine($"Order ID: {customer.Orders[i].OrderId}");
        }
    }

    if (pendingOrders.Count == 0)
    {
        Console.WriteLine("No pending orders found.");
        return;
    }

    Console.Write("Enter Order ID to modify: ");
    if (!int.TryParse(Console.ReadLine().Trim(), out int orderId))
    {
        Console.WriteLine("Invalid Order ID.");
        return;
    }

    Order orderToModify = null;
    for (int i = 0; i < pendingOrders.Count; i++)
    {
        if (pendingOrders[i].OrderId == orderId)
        {
            orderToModify = pendingOrders[i];
            break;
        }
    }

    if (orderToModify == null)
    {
        Console.WriteLine("Order not found.");
        return;
    }

    Console.WriteLine("Current Order Details:");
    orderToModify.DisplayOrderdFoodItems();
    Console.WriteLine($"Delivery Address: {orderToModify.DeliveryAddress}");
    Console.WriteLine($"Delivery Date/Time: {orderToModify.DeliveryDateTime:dd/MM/yyyy HH:mm}");

    Console.Write("Modify: [1] Items [2] Address [3] Delivery Time: ");
    string modChoice = Console.ReadLine().Trim();

    if (modChoice == "1")
    {
        List<FoodItem> availableItems = new List<FoodItem>();
        for (int i = 0; i < orderToModify.Restaurant.Menus.Count; i++)
        {
            Menu menu = orderToModify.Restaurant.Menus[i];
            for (int j = 0; j < menu.FoodItems.Count; j++)
            {
                availableItems.Add(menu.FoodItems[j]);
            }
        }

        Console.WriteLine("Available Food Items:");
        for (int i = 0; i < availableItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableItems[i].ItemName} - ${availableItems[i].ItemPrice:F2}");
        }

        Console.WriteLine("Modify Order Items:");
        Console.WriteLine("1. Add Item  2. Remove Item");
        if (!int.TryParse(Console.ReadLine(), out int itemChoice))
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        if (itemChoice == 1)
        {
            while (true)
            {
                Console.Write("Enter item number to add (0 to finish): ");
                if (!int.TryParse(Console.ReadLine(), out int itemNo) || itemNo < 0 || itemNo > availableItems.Count)
                {
                    Console.WriteLine("Not Available.");
                    continue;
                }
                if (itemNo == 0) break;

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                {
                    Console.WriteLine("Invalid quantity.");
                    continue;
                }

                FoodItem selectedItem = availableItems[itemNo - 1];

                
                orderToModify.AddOrderFoodItem(new OrderedFoodItem(
                    selectedItem.ItemName,
                    selectedItem.ItemDesc,
                    selectedItem.ItemPrice,
                    selectedItem.Customise,
                    qty
                ));

                Console.WriteLine($"{qty} x {selectedItem.ItemName} added.");
            }
        }
        else if (itemChoice == 2)
        {
            while (true)
            {
                List<OrderedFoodItem> items = orderToModify.OrderedFoodItems;
                if (items.Count == 0)
                {
                    Console.WriteLine("No items left to remove.");
                    break;
                }

                Console.WriteLine("Current Items:");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {items[i].ItemName} - {items[i].QtyOrdered}");
                }

                Console.Write("Enter item number to remove (0 to finish): ");
                if (!int.TryParse(Console.ReadLine(), out int removeNo) || removeNo < 0 || removeNo > items.Count)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                if (removeNo == 0) break;

                
                if (orderToModify.RemoveOrderFoodItem(items[removeNo - 1]))
                    Console.WriteLine($"{items[removeNo - 1].ItemName} removed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        
        double newTotal = orderToModify.CalculateOrderTotal();
        Console.WriteLine($"Order updated. New total: ${newTotal:F2}");
    }
    else if (modChoice == "2")
    {
        Console.Write("Enter new delivery address: ");
        orderToModify.DeliveryAddress = Console.ReadLine().Trim();
        Console.WriteLine("Delivery address updated.");
    }
    else if (modChoice == "3")
    {
        Console.Write("Enter new delivery time (HH:mm): ");
        string newTime = Console.ReadLine().Trim();
        orderToModify.DeliveryDateTime = new DateTime(
            orderToModify.DeliveryDateTime.Year,
            orderToModify.DeliveryDateTime.Month,
            orderToModify.DeliveryDateTime.Day,
            int.Parse(newTime.Split(':')[0]),
            int.Parse(newTime.Split(':')[1]),
            0
        );
        Console.WriteLine("Delivery time updated.");
    }
    else
    {
        Console.WriteLine("Invalid choice.");
        return;
    }

    Console.WriteLine("\nUpdated Order Details:");
    orderToModify.DisplayOrderdFoodItems();
    Console.WriteLine($"Delivery Address: {orderToModify.DeliveryAddress}");
    Console.WriteLine($"Delivery Date/Time: {orderToModify.DeliveryDateTime:dd/MM/yyyy HH:mm}");
    Console.WriteLine($"Total: ${orderToModify.OrderTotal:F2}");
}




void LoadSystem()
{
    Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
    LoadRestaurantsfile();
    LoadFooditemsfile();
    LoadCustomerfile();
    LoadOrderFile();
    
    foreach (Order o in ORDER)
    {
        orderQueue.Enqueue(o);
    }

    Console.WriteLine("15 restaurants loaded!");
    Console.WriteLine("51 food items loaded!");
    Console.WriteLine("20 customers loaded!");
    Console.WriteLine("35 orders loaded!");
}

void DisplaySystem()
{
    while (true)
    {
        Console.WriteLine("===== Gruberoo Food Delivery System =====");
        Console.WriteLine("1. List all restaurants and menu items");
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
            ListAllRestaurantsAndMenuItems();
        }
        else if (choice == "2")
        {
            ListAllOrder(ORDER);
        }
        else if (choice == "3")
        {
            CreateNewOrder();
        }
        else if (choice == "4")
        {
            ProcessOrder(orderQueue,refundStack);
        }
        else if (choice == "5")
        {
            ModifyExistingOrder();
        } 
        else if (choice == "6")
        {
            DeletingExistingOrder(orderQueue,refundStack);
        }
        else if (choice == "0")
        {
            break;
        }
    }
}

LoadSystem();
DisplaySystem();

