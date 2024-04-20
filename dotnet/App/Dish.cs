using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Food {
    public class Ingredient
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public static List<Ingredient> convertIngredients(string json){
            List<Ingredient> ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(json);
            return ingredients;
        }
        public Ingredient(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Dish
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CookingTime { get; set; }
        public string ingredientsString { get; set;}
        private List<Ingredient> Ingredients { get; set; }
        public double Price { get; set; }
        
        public static List<Dish> convertDishes(string json, List<Ingredient> allIngredients){
            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(json);
            foreach (var dish in dishes){
                string[] array = dish.ingredientsString.Split(',');    
                foreach( var item in array){     
                    Ingredient foundItem = allIngredients.FirstOrDefault(el => el.Name == item);
                    if (foundItem != null){
                        dish.Ingredients.Add(foundItem);
                    }
                    else{
                        Console.WriteLine($"Item with name '{item}' not found");
                    }
                }     
                dish.calcPrice();
            } 
            return dishes;
        }
        
        public static int checkDish(List<Dish> availableDishes,string inputDish){
            int index = 0;
            bool found = false;
            foreach( var dish in availableDishes ){
                if( string.Equals(inputDish, dish.Name, StringComparison.OrdinalIgnoreCase) ){
                    found = true;
                }
                if( found == false ){
                    index++;
                }
            }
            if( !found ){
                index = -1;
            } 
            return index;
        }

        public void show(){
            Console.WriteLine( this.Name );
            Console.Write("    \"" + this.Description + '"');
            Console.Write( "  (" );
            foreach ( var item in this.Ingredients ){
                Console.Write( item.Name + ',' );
            }
            Console.Write( ") ---> " +  this.Price + '$');
            Console.WriteLine( "....[" +  this.CookingTime + "min]");
        }
        public void calcPrice(){
            double sum =0;
            foreach ( var item in this.Ingredients ){
                sum += item.Price;
            }    
            sum *= 1.2;
            this.Price = (double)Math.Round(sum,2);
        }
        public Dish(string name, string description, int cookingtime , string ingredients)
        {
            Name = name;
            Description = description;
            CookingTime = cookingtime;
            ingredientsString = ingredients;
            Ingredients = new List<Ingredient>();
            //Price = calcPrice();
        }

    }
}
