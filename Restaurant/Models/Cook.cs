using Restaurant.Moels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Cook
    {
       // public delegate void ProcessedDelegate(TableRequests table);
       // public event ProcessedDelegate Processed;
        public void Process(TableRequests table)
        {
            var foods = table.Get<Food>();
            var prepareTask = new List<Task>();
            foreach (Food food in foods)
            {
                var task = new Task(() => food.Prepare());
                prepareTask.Add(task);
            }
            Task.WhenAll(prepareTask);
        }
    }
}
