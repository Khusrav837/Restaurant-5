using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    //TODO: Cook1, Cook2 classes? You can create just one Cook class and create 2 objects from it.
    public class Cook1
    {
        object locker = new object();
        public bool l = false; 
        public void Process(TableRequests table)
        {
            lock(locker)
            {
                l = true;
                var foods = table.Get<Food>();
                Parallel.ForEach(foods, food =>
                {
                    Food f = (Food)food;
                    f.Prepare();
                });
                Thread.Sleep(1000);
                l = false;
            }
        }
    }

    public class Cook2
    {
        object locker = new object();
        public bool l = false;
        public void Process(TableRequests table)
        {
            lock (locker)
            {
                l = true;
                var foods = table.Get<Food>();
                Parallel.ForEach(foods, food =>
                {
                    Food f = (Food)food;
                    f.Prepare();
                });
                Thread.Sleep(30000);
                l = false;
            }
        }
    }
}
