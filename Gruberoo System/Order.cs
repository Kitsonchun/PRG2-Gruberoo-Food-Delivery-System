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