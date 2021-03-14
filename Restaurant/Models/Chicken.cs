using Restaurant.Models;
using System.Threading.Tasks;

namespace Restaurant.Moels
{
    public class Chicken : Food
    {
        public Chicken(int quantity)
        {
            this.Quantity = quantity;
        }

        public override void Cook() { }

        public void CutUp() { }

        public override void Obtain() { }

        public override void Prepare()
        {
            Parallel.Invoke(
            Obtain,
            CutUp,
            Cook
            );
        }

        public override void Serve() { }
    }
}
