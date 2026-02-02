using Gruberoo_System;

void DisplaySystem()
{
    Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
    LoadRestrauntfile();
    LoadFooditemsfile();
    LoadCustomerfile();
    LoadOrdersfile();
    Console.WriteLine("15 restaurants loaded!");
    Console.WriteLine("51 food items loaded!");
    Console.WriteLine("20 customers loaded!");
    Console.WriteLine("35 orders loaded!");
}
