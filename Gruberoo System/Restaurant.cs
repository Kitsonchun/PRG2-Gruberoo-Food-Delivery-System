using System;
using System.Collections.Generic;

public class Restaurant 
{
    public string RestaurantID { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantEmail { get; set; }
    private List<Order> order = new List<Order>();
    private List<SpecialOffer> Specials = new List<SpecialOffer>();
    private List<Menu> menus = new List<Menu>();


    public Menu(string restaurantid, string restaurantname, string restaurantemail)
    {
        RestaurantID = restaurantid;
        RestaurantName = restaurantname;
        RestaurantEmail = restaurantemail
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
        foreach (var s in Specials)
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
        menus.Add(menus)
    }

    public bool RemoveMenu(menu)
    {

    }

    public override string ToString()
    {
        return ($"Restaurant: {RestaurantName} ({RestaurantID})");
    }



}
