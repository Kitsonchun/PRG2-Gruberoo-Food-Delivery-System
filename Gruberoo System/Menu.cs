using System;
using System.Collections.Generic;

public class Menu
{

	public string MenuID { get; set; }
	public string MenuName { get; set; }

	private List<FoodItem> fooditems = new List<FoodItem>();

	public Restaurant Restaurant { get; set; }

    public Menu(string menuid, string menuname)
	{
		MenuName = menuname;
		MenuID = menuid;
	}

	public void AddFoodItem(FoodItem items)
	{
		fooditems.Add(items);
	}

	public bool RemoveFoodItem(FoodItem item)
	{
		for (int i = 0; i < fooditems.Count; i++)
		{
			if (fooditems[i].ItemName == item.ItemName)
			{
				fooditems.RemoveAt(i);
				return true;
			}
		}

		Console.WriteLine("Food Item not found");
		return false;
	} 

	public void DisplayMenuItems()
	{
		foreach (var i in fooditems)
		{
			Console.WriteLine(i); 
		}

}

public override string ToString()
	{
		return ($"-{MenuName}");
	}
}

