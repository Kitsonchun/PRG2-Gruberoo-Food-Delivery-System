using System;

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
		if (fooditems = str.contains(items))
		{
			fooditems.Remove(items);
			return true;
		}
		else 
		{
			Console.writeline("Food Item not found")
            return false;
        }
			


    }

	public void DisplayMenuItems()
	{
		foreach (i in fooditems)
		{
			console.Writeline(i);
		}
	}

	public override string ToString()
	{
		return ($"-{MenuName}");
	}
}
}
