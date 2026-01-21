using Gruberoo_System;
using System;
using System.Collections.Generic;

public class Restaurant 
{
    public string RestaurantID { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantEmail { get; set; }
    private List<Order> order = new List<Order>();
    private List<SpecialOffer> specials = new List<SpecialOffer>();
    private List<Menu> menus = new List<Menu>();


    public Restaurant(string restaurantid, string restaurantname, string restaurantemail)
    {
        RestaurantID = restaurantid;
        RestaurantName = restaurantname;
        RestaurantEmail = restaurantemail;
    }

    public void DisplayOrders()
    {
        foreach (var o in order)
        {
            Console.WriteLine(o);
        }
    }
    public void DisplaySpecialOffers()
    {
        foreach (var s in specials)
        {
            Console.WriteLine(s);
        }
    }
    public void DisplayMenu()
    {   
        foreach (var m in menus)
        {
            Console.WriteLine(m);
        }

    }
    public void AddMenu(Menu menu)
    {
        menus.Add(menu);
    }

    public bool RemoveMenu(Menu menu)
    {
        if (menus.Contains(menu))
        {
            menus.Remove(menu);
            return true;
        }
        else
        {
            Console.WriteLine("Menu not found");
            return false;
        }
    }

    public override string ToString()
    {
        return ($"Restaurant: {RestaurantName} ({RestaurantID})");
    }



}
