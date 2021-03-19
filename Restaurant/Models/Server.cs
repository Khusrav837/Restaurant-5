using Restaurant.Moels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Models
{

    public class Server
    {
        List<string> resultOfCooks;
        private TableRequests tableRequests;
        Boolean sendedToCook = false;
        Boolean served = false;

        //TODO: Here should be list of cooks. new List<Cook> {cook1, cook2}
        Cook1 cook1 = new Cook1();
        Cook2 cook2 = new Cook2();

        public Server()
        {
            resultOfCooks = new List<string>();
            tableRequests = new TableRequests();
        }
 
        //TODO: I received order from a customer and sent it. Then I want to receive 2nd order but UI blocked.
        public void Receive(string customerName, int chickenQuantity, int eggQuantity, object drink)
        {
            foreach (var _ in Enumerable.Range(1, chickenQuantity))
            {
                tableRequests.Add<Chicken>(customerName);
            }

            foreach (var _ in Enumerable.Range(1, eggQuantity))
            {
                tableRequests.Add<Egg>(customerName);
            }
            if (drink is Drinks)
            {
                var d = (Drinks)drink;
                if (d == Drinks.Coca_Cola)
                {
                    tableRequests.Add<Coca_Cola>(customerName);
                }
                else if (d == Drinks.Juice)
                {
                    tableRequests.Add<Juice>(customerName);
                }
                else if (d == Drinks.RC_Cola)
                {
                    tableRequests.Add<RC_Cola>(customerName);
                }
                else if (d == Drinks.Tea)
                {
                    tableRequests.Add<Tea>(customerName);
                }
            }

            sendedToCook = chickenQuantity > 0 || eggQuantity > 0 || drink != null;
        }
        public List<string> SendToCook()
        {//TODO: Code duplication. If all cooks are busy it should wait for 1 second and try again.
            if (!cook1.l)
            {
                sendedToCook = true;
                Task processTask = new Task(() => cook1.Process(tableRequests));
                processTask.Start();
                Thread.Sleep(3000);
                Task serveTask = processTask.ContinueWith(Serve);
                Thread.Sleep(3000);
                serveTask.Wait();
                return resultOfCooks;
            }
            else if (cook2.l)
            {
                sendedToCook = true;
                Task processTask = new Task(() => cook2.Process(tableRequests));
                processTask.Start();
                Thread.Sleep(3000);
                Task serveTask = processTask.ContinueWith(Serve);
                Thread.Sleep(3000);
                serveTask.Wait();
                return resultOfCooks;
            }
            throw new Exception("All coocker are busy please wait!");
        }

        public void Serve(Task task)
        {
            //TODO: Why this method has 'task' parameter? Should we use it?
            //TODO: This method is too long. Can you make it small? The way to make it smaller is to use LINQ instead of 'foreach'...
            if (served)
            {
                throw new Exception("Customers already served!");
            }
            if (!sendedToCook)
            {
                throw new Exception("You didn't cook!");
            }

            foreach (KeyValuePair<string, List<IMenuItem>> row in tableRequests)
            {
                int ch = row.Value.Count(r => r is Chicken);
                int e = row.Value.Count(r => r is Egg);
                Type t = row.Value.Where(r => r is Drink).First().GetType();

                var str = $"Customer {row.Key} is served {ch} chicken, {e} egg, ";

                if (t != null)
                {
                    str += $"{t.Name}";
                }
                else
                {
                    str += "no drinks";
                }
                resultOfCooks.Add(str);
            }
            served = false;
            tableRequests.Clear();
        }

        public void Clear()
        {
            resultOfCooks.Clear();
        }
    }

    public enum Drinks : short
    {
        Tea,
        Juice,
        RC_Cola,
        Coca_Cola
    }
}
