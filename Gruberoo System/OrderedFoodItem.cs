using System;

public class OrderedFoodItem : FoodItem
{
	public int QtyOrdered { get; set; }
	public double SubTotal { get; set; }

	public double CalculateSubTotal()
	{
        double total;

        total = QtyOrdered * ItemPrice;

		return total;
    }

    

	public OrderedFoodItem(string itemName, string itemDesc, double itemPrice, string customise, int qtyOrdered) : base(itemName, itemDesc, itemPrice, customise)
	{
		QtyOrdered = qtyOrdered;
		SubTotal = CalculateSubTotal();

    }
}
