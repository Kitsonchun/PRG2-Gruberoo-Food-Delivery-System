using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Collections.Generic;
using Gruberoo_System;

//==========================================================
// Student Number : S10275142J
// Student Name   : Kitson Chun Yi Fan
// Partner Name   : Joel Ong Jia Jun 
//==========================================================
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

    public List<OrderedFoodItem> OrderedFoodItems { get; set; }
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

    // Change this to display number 7 Ordered Items//
    public void DisplayOrderdFoodItems()
    {
        Console.WriteLine($"{OrderId,-11}{Customer.CustomerName,-14}{Restaurant.RestaurantName,-18}{DeliveryDateTime,-21:dd/MM/yyyy HH:mm}${OrderTotal,-8:F2}{OrderStatus}");
    }

    public override string ToString()
    {
        return ($"Order {OrderId} confirmed. Status: {OrderStatus}");
    }



    public Order(int orderId, Customer customer, Restaurant restaurant,
                 DateTime deliveryDateTime, string deliveryAddress,
                 double orderTotal, string orderStatus)
    {
        OrderId = orderId;
        Customer = customer;
        Restaurant = restaurant;
        DeliveryDateTime = deliveryDateTime;
        DeliveryAddress = deliveryAddress;
        OrderTotal = orderTotal;
        OrderStatus = orderStatus;

        OrderDateTime = DateTime.Now;
        OrderedFoodItems = new List<OrderedFoodItem>();
    }

    public Order() { }
}