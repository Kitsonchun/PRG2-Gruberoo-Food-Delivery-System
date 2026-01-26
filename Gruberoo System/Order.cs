using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Collections.Generic;
using Gruberoo_System;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDateTime { get; set; }
    public double OrderTotal { get; set; }
    public string OrderStatus { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public string DeliveryAddress { get; set; }
    public string OrderPaymentMethod { get; set; }
    public bool OrderPaid { get; set; }

    private List<OrderedFoodItem> orderedFoodItems = new List<OrderedFoodItem>();

    public OrderedFoodItem OrderedFoodItems { get; set; }

    public Restaurant Restaurant { get; set; }

    public SpecialOffer SpecialOffer { get; set; }

    public Customer Customer { get; set; }

    public double CalculateOrderTotal()
    {
        double Total = 0;
        foreach (OrderedFoodItem item in orderedFoodItems)
        {
            OrderTotal += item.CalculateSubTotal();
        }

        OrderTotal = Total;
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
        Console.WriteLine("Order ID   Customer    Restaurant     Delivery Date/Time   Amount   Status");
        Console.WriteLine("--------   --------    ----------     ------------------   ------   ---------");
        Console.WriteLine($"{OrderId}   {Customer.customerName}    {Restaurant.RestaurantName}     {DeliveryDateTime}   {OrderTotal}   {OrderStatus}");

        foreach (OrderedFoodItem order in orderedFoodItems)
        {
            Console.WriteLine(order);
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