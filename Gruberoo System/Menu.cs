using System;

	public class Menu 
	{
	
	public string MenuID { get; set; }
	public string MenuName { get; set; }
	private List<Fooditem> Fooditems = new List<Fooditem>();
	public Menu(string MenuID, string MenuName)
	{
		this.MenuName = MenuName;
		this.MenuID = MenuID;
	}

	public void AddFoodItem(Fooditems items)
	{
		
	}
	
	public bool RemoveFoodItem()
	{

	}

	public void DisplayMenuItems()
	{

	}

	public override string ToString()
	{
		return ($"- {MenuID}: {MenuName}");
	}
}
}
