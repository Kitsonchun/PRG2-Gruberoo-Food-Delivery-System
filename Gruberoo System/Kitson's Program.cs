using Gruberoo_System;

// Doing features 1, 4, 6 & 8 //

List<Restaurant> restaurants = new List<Restaurant>();
List<FoodItem> foodItems = new List<FoodItem>();
Dictionary<string, Menu> restaurantDict = new Dictionary<string, Menu>();

void LoadRestaurantsfile()
{ 
    string[] ResLines = File.ReadAllLines("restaurant.csv");

    foreach (string res in ResLines.Skip(1))
    {
        var parts = res.Split(',');
        string restaurantId = parts[0];
        string restaurantName = parts[1];
        string restaurantEmail = parts[2];

        Restaurant restaurant = new Restaurant(restaurantId, restaurantName, restaurantEmail);
        restaurants.Add(restaurant);

       
    }
}

void LoadFooditemsfile()
{
    string[] FoodLines = File.ReadAllLines("fooditems.csv");

    foreach (string food in FoodLines.Skip(1))
    {
        var parts = food.Split(',');
        string restaurantId = parts[0];
        string itemName = parts[1];
        string itemDesc = parts[2];
        double itemPrice = Convert.ToDouble(parts[3]);
        string customise = parts[4];

        FoodItem foodItem = new FoodItem(itemName, itemDesc, itemPrice, customise);

        foreach (Restaurant restaurant in restaurants)
        {
            if (restaurant.RestaurantID == parts[0])
            {
                restaurant.AddMenu(new Menu("Menu1", "Main Menu"));
                restaurant.Menu.AddFoodItem(foodItem);
            }
        }
    }


}
