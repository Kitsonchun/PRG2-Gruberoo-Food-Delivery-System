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
        string customise = " ";

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
    Console.WriteLine("Order ID   Customer      Restaurant        Delivery Date/Time   Amount   Status");
    Console.WriteLine("--------   --------      ----------        ------------------   ------   ---------");

    if (orders.Count == 0)
    {
        Console.WriteLine("No orders found.");
    }

    foreach (Order order in orders)
    {
        Console.WriteLine($"{order.OrderId,-11}{order.Customer.CustomerName,-14}{order.Restaurant.RestaurantName,-18}{order.DeliveryDateTime,-21:dd/MM/yyyy HH:mm}${order.OrderTotal,-8:F2}{order.OrderStatus}");
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
            Console.WriteLine($"Order {order.OrderId}");
            Console.WriteLine($"Customer: {order.Customer.CustomerName}");
            Console.WriteLine("Ordered Items:");
            
           order.DisplayOrderdFoodItems();
            
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
                    newQueue.Enqueue(order);
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
    Console.WriteLine();

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
                Console.WriteLine();
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

// Advance Feature A //
void BulkProccesing(Queue<Order> orderQueue)
{
    Console.WriteLine("Bulk Process Orders");
    Console.WriteLine("===================");
    Console.Write("Enter Restaurant ID: ");
    string ID = Convert.ToString(Console.ReadLine());

    int TotalPendingOrders = 0;

    foreach (Order order in orderQueue)
    {
        if (order.OrderStatus == "Pending")
        {
            TotalPendingOrders++;
        }
    }

    Console.WriteLine($"Total Pending Orders in Queue: {TotalPendingOrders}");

    if (TotalPendingOrders == 0)
    {
        Console.WriteLine("No pending orders to process");
    }

    int orderproccessed = 0;
    int preparingcount = 0;
    int rejectedcount = 0;

    Queue<Order> tempQueue = new Queue<Order>();
    DateTime currentTime = DateTime.Now;

    while (orderQueue.Count > 0)
    {
        Order order = orderQueue.Dequeue();

        if (order.OrderStatus == "Pending")
        {
            double hoursDifference = (order.DeliveryDateTime - currentTime).TotalHours;

            if (hoursDifference < 1)
            {
                order.OrderStatus = "Rejected";
                refundStack.Push(order);
                rejectedcount++;
                Console.WriteLine($"Order {order.OrderId} rejected (delivery time < 1 hour)");
            }

            else
            {
                order.OrderStatus = "Preparing";
                preparingcount++;
                Console.WriteLine($"Order {order.OrderId} confirmed (status: Preparing)");
            }

            orderproccessed++;
        }

        if (order.OrderStatus != "Rejected")
        {
            tempQueue.Enqueue(order);
        }
    }

    while (tempQueue.Count > 0)
    {
        orderQueue.Enqueue(tempQueue.Dequeue());
    }

    Console.WriteLine();
    Console.WriteLine("Summary Statistics:");
    Console.WriteLine("===================");
    Console.WriteLine($"Orders Proccessed: {orderproccessed}");
    Console.WriteLine($"Preparing: {preparingcount}");
    Console.WriteLine($"Rejected: {rejectedcount}");

    int totalOrders = 0;
    foreach (Order orders in ORDER)
    {
        totalOrders++;
    }

    if (totalOrders > 0)
    {
        double percentage = (orderproccessed / totalOrders) * 100;
        Console.WriteLine($"Percentage of automatically processed orders: {percentage:F2}%");
    }

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
       
        List<string> parts = new List<string>();
        bool inQuotes = false;
        string currentField = "";

        for (int i = 0; i < o.Length; i++)
        {
            char c = o[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                parts.Add(currentField);
                currentField = "";
            }
            else
            {
                currentField += c;
            }
        }
        parts.Add(currentField);

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


        string itemsData = parts.Count > 9 ? parts[9].Trim() : "";

        if (!string.IsNullOrEmpty(itemsData))
        {
            string[] items = itemsData.Split('|');

            foreach (string item in items)
            {
                string trimmedItem = item.Trim();

                string[] itemParts = trimmedItem.Split(',');


                if (itemParts.Length >= 2)
                {
                    int quantity = int.Parse(itemParts[itemParts.Length - 1].Trim());


                    string itemName = "";
                    for (int i = 0; i < itemParts.Length - 1; i++)
                    {
                        if (i > 0) itemName += ",";
                        itemName += itemParts[i].Trim();
                    }


                    foreach (Menu menu in restaurant.Menus)
                    {
                        foreach (FoodItem foodItem in menu.FoodItems)
                        {
                            if (foodItem.ItemName == itemName)
                            {
                                OrderedFoodItem orderedItem = new OrderedFoodItem(
                                    foodItem.ItemName,
                                    foodItem.ItemDesc,
                                    foodItem.ItemPrice,
                                    foodItem.Customise,
                                    quantity
                                );
                                order.AddOrderFoodItem(orderedItem);
                                break;
                            }
                        }
                    }
                }
            }
        }

        ORDER.Add(order);
        customer.AddOrder(order);
        restaurant.order.Add(order);

        if (status == "Pending")
        {
            orderQueue.Enqueue(order);
        }
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
                Console.WriteLine($"- {item.ItemName}: {item.ItemDesc} - ${item.ItemPrice:F2}");
            }
        }

        Console.WriteLine();
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
        Console.WriteLine("Restaurant not found"); return;
    }

    DateTime createdDateTime = DateTime.Now;


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
        
    }
    String SpecialRequest = "";
    while (true)
    {
        Console.Write("Add special request? [Y/N]: ");
        string specialChoice = Console.ReadLine().Trim().ToUpper();

        if (specialChoice == "Y")
        {
            Console.Write("Enter your special request: ");
            SpecialRequest = Console.ReadLine().Trim();

        }
        else if (specialChoice == "N")
        {
            SpecialRequest = "";
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter Y or N.");
        }
    }


    double orderTotal = newOrder.CalculateOrderTotal() + 5;

    Console.WriteLine($"Order Total: ${newOrder.CalculateOrderTotal():F2} + $5.00 (delivery) = ${orderTotal:F2}");

    bool proceedToPayment = false;
    while (true)
    {
        Console.Write("Proceed to payment? [Y/N]: ");
        string proceedInput = Console.ReadLine().Trim().ToUpper();

        if (proceedInput == "Y")
        {
            proceedToPayment = true;
            break;
        }
        else if (proceedInput == "N")
        {
            proceedToPayment = false;
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter Y or N.");
        }
    }

    if (!proceedToPayment)
    {
        Console.WriteLine("Order cancelled");
        return;
    }

    Console.WriteLine();
    string Paymentmethod = "";
    while (true)
    {
        Console.Write("[CC] Credit Card / [PP] PayPal / [CD] Cash on Delivery: ");
        Paymentmethod = Console.ReadLine().Trim().ToUpper();

        if (Paymentmethod == "CC" || Paymentmethod == "PP" || Paymentmethod == "CD")
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid payment method. Please choose CC, PP, or CD.");
        }
    }

    Console.WriteLine($"Payment method selected: {Paymentmethod}");
    string itemsInOrder = string.Join(" | ", newOrder.OrderedFoodItems.Select(item => $"{item.ItemName}, {item.QtyOrdered}"));
    newOrder.OrderPaymentMethod = Paymentmethod;
    customer.AddOrder(newOrder);
    ORDER.Add(newOrder);
    restaurant.order.Add(newOrder);
    orderQueue.Enqueue(newOrder);

    string filePath = @"C:\Users\ongjj\OneDrive\Documents\GitHub\PRG2-Gruberoo-Food-Delivery-System\Gruberoo System\orders.csv";
    string orderLine = $"{newOrder.OrderId},{customer.EmailAddress},{restaurant.RestaurantID},{deliveryDateTime:dd/MM/yyyy},{deliveryDateTime:HH:mm},{deliveryAddress},{createdDateTime:dd/MM/yyyy HH:mm},{orderTotal:F2},{newOrder.OrderStatus},{itemsInOrder}";
    File.AppendAllText(filePath, orderLine + Environment.NewLine);

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

    List<Order> PendingOrders = new List<Order>();
    for (int i = 0; i < customer.Orders.Count; i++)
    {
        if (customer.Orders[i].OrderStatus == "Pending")
        {
            PendingOrders.Add(customer.Orders[i]);
            Console.WriteLine($"Order ID: {customer.Orders[i].OrderId}");
        }
    }

    if (PendingOrders.Count == 0)
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

    Order OrderModify = null;
    for (int i = 0; i < PendingOrders.Count; i++)
    {
        if (PendingOrders[i].OrderId == orderId)
        {
            OrderModify = PendingOrders[i];
            break;
        }
    }

    if (OrderModify == null)
    {
        Console.WriteLine("Order not found.");
        return;
    }

    Console.WriteLine("Order Items:");
    OrderModify.DisplayOrderdFoodItems();
    Console.WriteLine("Delivery Address:");
    Console.WriteLine($"{OrderModify.DeliveryAddress}");
    Console.WriteLine($"Delivery Date/Time:");
    Console.WriteLine($"{OrderModify.DeliveryDateTime:dd/MM/yyyy HH:mm}");

    Console.WriteLine();
    Console.Write("Modify: [1] Items [2] Address [3] Delivery Time: ");
    string modChoice = Console.ReadLine().Trim();

    if (modChoice == "1")
    {
        List<FoodItem> availableItems = new List<FoodItem>();
        for (int i = 0; i < OrderModify.Restaurant.Menus.Count; i++)
        {
            Menu menu = OrderModify.Restaurant.Menus[i];
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
        Console.Write("1. Add Item  2. Remove Item");
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

                
                OrderModify.AddOrderFoodItem(new OrderedFoodItem(
                    selectedItem.ItemName,
                    selectedItem.ItemDesc,
                    selectedItem.ItemPrice,
                    selectedItem.Customise,
                    qty
                ));


                Console.WriteLine();
                Console.WriteLine($"Order: {OrderModify.OrderId} updated. {qty} x {selectedItem.ItemName} added");
            }
        }
        else if (itemChoice == 2)
        {
            while (true)
            {
                List<OrderedFoodItem> items = OrderModify.OrderedFoodItems;
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

                
                if (OrderModify.RemoveOrderFoodItem(items[removeNo - 1]))
                    Console.WriteLine($"{items[removeNo - 1].ItemName} removed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        
        double newTotal = OrderModify.CalculateOrderTotal();
        Console.WriteLine($"Order updated. New total: ${newTotal:F2}");
    }
    else if (modChoice == "2")
    {
        Console.Write("Enter new delivery address: ");
        OrderModify.DeliveryAddress = Console.ReadLine().Trim();

        Console.WriteLine();
        Console.WriteLine($"Order: {OrderModify.OrderId} updated. Delivery Address: {OrderModify.DeliveryAddress}");
    }
    else if (modChoice == "3")
    {
        Console.Write("Enter new delivery time (HH:mm): ");
        string newTime = Console.ReadLine().Trim();
        OrderModify.DeliveryDateTime = new DateTime(
            OrderModify.DeliveryDateTime.Year,
            OrderModify.DeliveryDateTime.Month,
            OrderModify.DeliveryDateTime.Day,
            int.Parse(newTime.Split(':')[0]),
            int.Parse(newTime.Split(':')[1]),
            0
        );

        Console.WriteLine();
        Console.WriteLine($"Order: {OrderModify.OrderId} updated. Delivery Date/Time: {OrderModify.DeliveryDateTime:HH:mm}");
    }

    else
    {
        Console.WriteLine("Invalid choice.");
        return;
    }
 
}

//Advanced Feature B//
void TotelGruberooEarnings()
{
    Console.WriteLine("Gruberoo Earnings Summary");
    Console.WriteLine("=========================");

    double TotalOrderamt = 0;
    double Totalrefunds = 0;
    double DeliveryFee = 5.0;

    for (int i = 0; i < restaurants.Count; i++)
    {
        Restaurant Restaurant = restaurants[i];

        double RestaurantOrderTotal = 0;
        double RestaurantRefundTotal = 0;

        Console.WriteLine($"\nRestaurant: {Restaurant.RestaurantName}");

        for (int o = 0; o < Restaurant.order.Count; o++)
        {
            Order order = Restaurant.order[o];

            if (order.OrderStatus == "Delivered")
            {
                double Totamt = order.OrderTotal - DeliveryFee;
                RestaurantOrderTotal += Totamt;
            }
            else if (order.OrderStatus == "Rejected")
            {
                RestaurantRefundTotal += order.OrderTotal;
            }
        }

        Console.WriteLine($"Total Order Amount (Excluding delivery fees): ${RestaurantOrderTotal:F2}");
        Console.WriteLine($"Total Refunds: ${RestaurantRefundTotal:F2}");

        TotalOrderamt += RestaurantOrderTotal;
        Totalrefunds += RestaurantRefundTotal;
    }

    double finalEarnings = TotalOrderamt - Totalrefunds;

    Console.WriteLine("\n=========================");
    Console.WriteLine($"Total Order Amount: ${TotalOrderamt:F2}");
    Console.WriteLine($"Total Refunds: ${Totalrefunds:F2}");
    Console.WriteLine($"Final Amount Gruberoo Earns: ${finalEarnings:F2}");
}



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
    while (true)
    {
        Console.WriteLine("===== Gruberoo Food Delivery System =====");
        Console.WriteLine("1. List all restaurants and menu items");
        Console.WriteLine("2. List all orders");
        Console.WriteLine("3. Create a new order");
        Console.WriteLine("4. Process an order");
        Console.WriteLine("5. Modify an existing order");
        Console.WriteLine("6. Delete an existing order");
        Console.WriteLine("7. Unprocessed orders for a current day");
        Console.WriteLine("8. DIsplay Total Gruberoo Earnings");
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
        else if (choice == "7")
        {
            //PUT HERE MY BOY//
        }
        else if (choice == "8")
        {
            TotelGruberooEarnings();
        }
        else if (choice == "0")
        {
            break;
        }
    }
}

LoadSystem();
DisplaySystem();

