using System;
using System.Threading;
using Food;
using Cooking;

namespace Managing{
    class Manager{

        public List<Chef> Chefs { get; set; }
        public Thread processingOrders { get; set; }
        public Dish newOrder  { get; set; }
        public int maxOrdersPerCook  { get; set; }


        private int checkAvailableCook(){
            int minTasks = maxOrdersPerCook;
            int index = 0;
            int freeChef=-1;
            foreach(var chef in Chefs){
                Console.Write( chef.Name + ' ' + chef.AcceptedOrders + "/5  |  "   );
                if( chef.AcceptedOrders < minTasks ){
                    minTasks = chef.AcceptedOrders;
                    freeChef = index;
                }    
                index++;
            }    
             Console.WriteLine();
            return freeChef;
        }

        public void insertOrder(Dish obj){
            newOrder = obj;

        }
        private void processOrder(){
            while(true){
                if( newOrder != null ){ 
                    Thread.Sleep(500);
                    int chefIndex = checkAvailableCook();
                    if( chefIndex != -1 ){
                        Chefs[chefIndex].newOrder( newOrder );
                        int deliveryTime = Chefs[chefIndex].deliveryAllOrdersTime() - (int)(DateTime.Now - Chefs[chefIndex].lastUpdate).TotalSeconds;
                        Console.WriteLine($"~ Order Accepted by {Chefs[chefIndex].Name} ! Estimated time: {deliveryTime}");
                    }
                    else{
                        Console.WriteLine("Order Denied, there is no free cook !");
                    }  
                    newOrder = null;
                }
            }
        }

        public Manager(int chefs_nr , string[] names ){
            maxOrdersPerCook = 5;
            newOrder = null;
            processingOrders = new Thread(new ThreadStart(processOrder));
            processingOrders.Start();
            Chefs = new List<Chef>();
            for(int i = 0; i<chefs_nr; i++){
                Chefs.Add( new Chef( names[i] ) );
            }

        }

    }
}
