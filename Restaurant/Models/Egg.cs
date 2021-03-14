using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Moels
{
    public class Egg : Food, IDisposable
    {
        private int _quality;

        public int Quality
        {
            get { return _quality; }
            private set { _quality = value; }
        }

        private Boolean _isDisposed = false;

        private Boolean IsDisposed
        {
            get => _isDisposed;
            set => _isDisposed = value;
        }

        public Egg(int quantity)
        {
            Random rand = new Random();
            this.Quality = rand.Next(101);
            this.Quantity = quantity;
        }

        public int GetQuality()
        {
            return this.Quality;
        }

        public void Crack()
        {
            if (this.Quality < 25)
            {
                throw new Exception("Quality is less!");
            }
        }

        private void DiscardShell() { }

        protected void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                this.DiscardShell();
                if (disposing)
                {
                    Console.WriteLine("Cleaning!");
                }
                Console.WriteLine("Cleaning!");
                this.IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override void Cook() { }

        public override void Obtain() { }

        public override void Serve() { }

        public override void Prepare()
        {
            this.Obtain();
            try
            {
                this.Crack();
            }
            catch
            {
            }
            this.Cook();
        }

        ~Egg()
        {
            this.Dispose(false);
        }
    }
}
