using System;
using System.Collections.Generic;
public class Menu 
	{
	
	public string MenuID { get; set; }
	public string MenuName { get; set; }
	private List<Fooditem> fooditems = new List<Fooditem>();
	public Menu(string menuid, string menuname)
	{
		MenuName = menuname;
		MenuID = menuid;
	}

	public void AddFoodItem(Fooditems items)
	{
		fooditems.Add(items);
	}
	
	public bool RemoveFoodItem(Fooditems items)
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
			Console.Writeline(i);
		}
	}

	public override string ToString()
	{
		return ($"-{MenuName}");
	}
}
}
