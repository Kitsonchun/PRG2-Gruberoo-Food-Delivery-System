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

    public List<OrderedFoodItem> OrderedFoodItems { get; set; }
    public Restaurant Restaurant { get; set; }

    public SpecialOffer SpecialOffer { get; set; }

    public Customer Customer { get; set; }

    public double CalculateOrderTotal()
    {
        double Total = 0;

        foreach (OrderedFoodItem item in OrderedFoodItems)
        {
            Total += item.CalculateSubTotal();
        }

        OrderTotal = Total;
        return Total;
    }

    public void AddOrderFoodItem(OrderedFoodItem Item)
    {
        OrderedFoodItems.Add(Item);
    }

    public bool RemoveOrderFoodItem(OrderedFoodItem Item)
    {
        return OrderedFoodItems.Remove(Item);
    }

    // Change this to display number 7 Ordered Items//
    public void DisplayOrderdFoodItems()
    {
        int i = 1;

        foreach (OrderedFoodItem item in OrderedFoodItems)
        {
            Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
            i++;
        }
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