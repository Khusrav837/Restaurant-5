using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Cook
    {
        public void Process(TableRequests table)
        {
            var foods = table.Get<Food>();
            //TODO: What about using locking the Cook? See slide #15.
            var prepareTask = new List<Task>();
            //TODO: Can we use Paraller.Foreach... here?
            foreach (Food food in foods)
            {
                var task = new Task(() => food.Prepare());
                prepareTask.Add(task);
            }
            Task.WhenAll(prepareTask);
        }
    }
}
