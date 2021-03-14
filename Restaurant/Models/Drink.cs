namespace Restaurant.Models
{
    public abstract class Drink : IMenuItem
    {
        abstract public void Obtain();
        abstract public void Serve();
    }

    public class Tea : Drink
    {
        public override void Obtain() {}
        public override void Serve() {}
    }

    public class Juice : Drink
    {
        public override void Obtain() { }
        public override void Serve() { }
    }

    public class RC_Cola : Drink
    {
        public override void Obtain() { }
        public override void Serve() { }
    }

    public class Coca_Cola : Drink
    {
        public override void Obtain() { }
        public override void Serve() { }
    }
}
