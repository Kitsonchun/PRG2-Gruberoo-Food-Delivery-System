using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

public class Order
{

    int OrderId { get; set; }
    DateTime OrderDateTime { get; set; }
    double OrderTotal { get; set; }
    string OrderStatus { get; set; }
    DateTime DelveryDateTime { get; set; }
    string DeliveryAddress { get; set; }
    string OrderPaymentMethod { get; set; }
    bool OrderPaid { get; set; }

    public double CalculateOrderTotal()
    {

    }

    public void AddOrderFoodItem(string OrderedFoodItem)
    {

    }

    public bool RemoveOrderFoodItem(string OrderedFoodItem)
    {

    }

    public void DisplayOrderdFoodItems()
    {

    }

    public string ToString()
    {

    }



    public Order()
    {

    }
}