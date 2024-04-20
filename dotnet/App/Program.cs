using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Food;
using Managing;
using fileswork;

class Program
{
   
    static void showMenu(List<Dish> availableDishes){
        Console.WriteLine();
        Console.WriteLine("        -------------------Menu-------------------");
        Console.WriteLine();
        int index = 1;
        foreach( var dish in availableDishes ){
            Console.Write( index.ToString() + ".  ");
            dish.show();
            Console.WriteLine();
            index++;
        }
        Console.WriteLine();
        Console.WriteLine("0. Exit");
    }

    static void Main()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string ingredientsPath = basePath + @"data\ingredients.txt";
        string dishesPath = basePath + @"data\dishes.txt";

        fileswork.File f1 = new fileswork.File(ingredientsPath);
        fileswork.File f2 = new fileswork.File(dishesPath);

        string ingredientsJSON  = f1.readFile() ;
        string dishesJSON  = f2.readFile() ;

        List<Ingredient> allIngredients = Ingredient.convertIngredients(ingredientsJSON);
        List<Dish> allDishes = Dish.convertDishes(dishesJSON,allIngredients);

        string message = "";
        bool exit = false;
        int orderedDish;
        
        Manager m1 = new Manager(1,["Alex"]);

        DateTime currentTime = DateTime.Now;
        Console.WriteLine(currentTime);

        while (!exit)
        {
            orderedDish = -1;
            showMenu(allDishes);

            Console.WriteLine(message);
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            
            if(input != null ){
                if( input == "0"){
                exit = true;
                }
                else {
                    orderedDish = Dish.checkDish(allDishes, input);
                    if( orderedDish != -1 ){
                        message = "  ** Order placed --> " + allDishes[orderedDish].Name;
                        m1.insertOrder( allDishes[orderedDish] );

                    }
                    else{
                        message="Sorry, we don't cook this dish.";
                    }
                }
            Console.Clear();
            }    
        }


    }
}