namespace Restaurant.Models
{
    public abstract class Food : IMenuItem
    {

        protected int Quantity { get; set; }

        public abstract void Cook();

        public int GetQuantity()
        {
            return this.Quantity;
        }
        public abstract void Prepare();
        public abstract void Obtain();

        public abstract void Serve();
    }
}
