using System;
using System.Collections.Generic;

public class Menu 
	{
	
	public string MenuID { get; set; }
	public string MenuName { get; set; }
	private List<FoodItem> fooditems = new List<FoodItem>();
	public Menu(string menuid, string menuname)
	{
		MenuName = menuname;
		MenuID = menuid;
	}

	public void AddFoodItem(FoodItem items)
	{
		fooditems.Add(items);
	}
	
	public bool RemoveFoodItem(FoodItem items)
	{
		if (fooditems.Contains(items))
		{
			fooditems.Remove(items);
			return true;
		}
		else 
		{
			Console.WriteLine("Food Item not found");
            return false;
        }
			


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
}
