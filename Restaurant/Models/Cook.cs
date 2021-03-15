using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Models
{
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
                Thread.Sleep(30000);
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
