using Gruberoo_System;
using System;
using System.Collections.Generic;

//==========================================================
// Student Number : S10275107B
// Student Name   : Joel Ong Jia Jun
// Partner Name   : Kitson Chun Yi Fan
//==========================================================
public class Restaurant 
{
    public string RestaurantID { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantEmail { get; set; }

    public List<Order> order = new List<Order>();
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

    public bool RemoveMenu(string menuId)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].MenuID == menuId)
            {
                menus.RemoveAt(i);
                return true;
            }
        }
        Console.WriteLine("Menu not found");
        return false;
    }


    public override string ToString()
    {
        return ($"Restaurant: {RestaurantName} ({RestaurantID})");
    }



}
