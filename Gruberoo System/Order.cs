using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDateTime { get; set; }
    public double OrderTotal { get; set; }
    public string OrderStatus { get; set; }
    public DateTime DelveryDateTime { get; set; }
    public string DeliveryAddress { get; set; }
    public string OrderPaymentMethod { get; set; }
    public bool OrderPaid { get; set; }

    private List<OrderedFoodItem> orderedFoodItems;

    public double CalculateOrderTotal()
    {
        double Total = 0;
        foreach (OrderedFoodItem item in orderedFoodItems)
        {
            OrderTotal += item.CalculateSubTotal();
        }

        return Total;
    }

    public void AddOrderFoodItem(OrderedFoodItem Item)
    {
        orderedFoodItems.Add(Item);
    }

    public bool RemoveOrderFoodItem(OrderedFoodItem Item)
    {
        return orderedFoodItems.Remove(Item);
    }

    public void DisplayOrderdFoodItems()
    {
        foreach (Order order in orders)
        {
            Console.WriteLine("Order ID   Customer    Restaurant     Delivery Date/Time   Amount   Status");
            Console.WriteLine("--------   --------    ----------     ------------------   ------   ---------");
            Console.WriteLine($"{order.OrderId}   {order.CustomerName}    {order.RestaurantName}     {order.DelveryDateTime}   {order.OrderTotal}   {order.OrderStatus}");
        }
    }

    public override string ToString()
    {
        return ($"Order {OrderId} confirmed. Status: {OrderStatus}");
    }



    public Order(int orderID)
    {
        OrderId = orderID;
        OrderDateTime = DateTime.Now;
        orderedFoodItems = new List<OrderedFoodItem>();
    }
}