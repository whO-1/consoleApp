using System;
using System.Threading;
using Food;


namespace Cooking{
    class Chef{

        public string Name { get; set; }
        public List<Dish> Orders { get; set; }
        public Thread CookDish { get; set; }
        public int AcceptedOrders { get; set; }

        public DateTime lastUpdate { get; set; }

        public void newOrder(Dish order){
            Orders.Add( order );
            AcceptedOrders++;
            if(AcceptedOrders == 1){
                lastUpdate = DateTime.Now;
            }
        }

        public int deliveryAllOrdersTime(){
            int sum = 0;
            foreach(var order in Orders){
                sum += order.CookingTime;
            }
            return sum;
        }
        private void Cooking(){
            while(true){
                if(AcceptedOrders > 0){
                    Console.WriteLine("Cooking " + Orders[0].Name + "..." );
                    Thread.Sleep(Orders[0].CookingTime*1000 );
                    Console.WriteLine("Done Cooking!");   
                    lastUpdate = DateTime.Now;
                    Orders.RemoveAt( 0 );  
                    AcceptedOrders--;
                }
            }
             
        }

        public Chef(string name){
            Name = name;
            Orders = new List<Dish>();
            CookDish = new Thread(new ThreadStart(Cooking));
            CookDish.Start();
            AcceptedOrders = 0;
        }

    }
}
