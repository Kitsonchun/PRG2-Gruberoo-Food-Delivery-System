using System;
using System.Collections.Generic;


//==========================================================
// Student Number : S10275142J
// Student Name   : Kitson Chun Yi Fan
// Partner Name   : Joel Ong Jia Jun 
//==========================================================
public class FoodItem
{
	public string ItemName { get; set; }
	public string ItemDesc { get; set; }
	public double ItemPrice { get; set; }
	public string Customise { get; set; }

	public Menu Menu { get; set; }
	
    public override string ToString()
	{
		return ($"- {ItemName}: {ItemDesc} - {ItemPrice}");
    }

    public FoodItem(string itemName,string itemDesc,double itemPrice,string customise)
	{
		ItemName = itemName;
		ItemDesc = itemDesc;
		ItemPrice = itemPrice;
		Customise = customise;
    }
}
