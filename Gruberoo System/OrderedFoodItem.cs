using System;

public class OrderedFoodItem : FoodItem
{
	int QtyOrdered { get; set; }
	double SubTotal { get; set; }

	public double CalculateSubTotal()
	{

	}

    public OrderedFoodItem()
	{
	}
}
