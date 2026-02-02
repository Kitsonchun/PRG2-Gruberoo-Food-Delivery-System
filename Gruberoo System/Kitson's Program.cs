using Gruberoo_System;
using System.Reflection.Metadata.Ecma335;

// Doing features 1, 4, 6 & 8 //

List<Restaurant> restaurants = new List<Restaurant>();
Dictionary<string,Menu> restaurantMenus = new Dictionary<string,Menu>();
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

LoadRestaurantsfile();
LoadFooditemsfile();