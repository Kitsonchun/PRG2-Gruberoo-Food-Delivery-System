using System;
using System.Collections.Generic;


//==========================================================
// Student Number : S10275142J
// Student Name   : Kitson Chun Yi Fan
// Partner Name   : Joel Ong Jia Jun 
//==========================================================
public class OrderedFoodItem : FoodItem
{
	public int QtyOrdered { get; set; }
	public double SubTotal { get; set; }

	public Order Order { get; set; }

    public double CalculateSubTotal()
	{ 
       return QtyOrdered * ItemPrice;
    }

	public OrderedFoodItem(string itemName, string itemDesc, double itemPrice, string customise, int qtyOrdered) : base(itemName, itemDesc, itemPrice, customise)
	{
		QtyOrdered = qtyOrdered;

    }
}
