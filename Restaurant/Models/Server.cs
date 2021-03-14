

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
        Cook cook = new Cook();
        //   public delegate void ReadyDelagate(TableRequests table);
        //   public event ReadyDelagate Ready;

        public Server()
        {
            resultOfCooks = new List<string>();
            tableRequests = new TableRequests();
        }
 
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
        {
            sendedToCook = true;
            Task processTask = new Task(() => cook.Process(tableRequests));
            processTask.Start();
            Thread.Sleep(3000);
            Task serveTask = processTask.ContinueWith(Serve);
            Thread.Sleep(3000);
            serveTask.Wait();
            return resultOfCooks;
        }
        public void Serve(Task task)
        {
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
                var ch = 0;
                var e = 0;
                Type t = null;

                foreach (IMenuItem value in row.Value)
                {
                    if (value is Chicken)
                    {
                        ch++;
                    }
                    else if (value is Egg)
                    {
                        e++;
                    }
                    else if (value is Drink)
                    {
                        t = value.GetType();
                    }
                }

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
