using System;

public class OrderedFoodItem : FoodItem
{
	public int QtyOrdered { get; set; }
	public double SubTotal { get; set; }

	public double CalculateSubTotal()
	{ 
       return QtyOrdered * ItemPrice;
    }

    

	public OrderedFoodItem(string itemName, string itemDesc, double itemPrice, string customise, int qtyOrdered) : base(itemName, itemDesc, itemPrice, customise)
	{
		QtyOrdered = qtyOrdered;

    }
}
