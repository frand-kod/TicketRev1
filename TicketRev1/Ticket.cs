using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRev1
{
    public abstract class Tiket : ITicket
    {
        private readonly string name;
        protected string id;

        public string Name { get { return name; } }
        public int Price { get; protected set; }
        public string Id { get { return id; } set { id = value; } }


        public Tiket(string name)
        {
            this.name = name;
            id = Convert.ToString(Utils.GenerateRandomId());//dapatkan id dari randomize generator
        }

        //deklarasi fungsu yang dapat di ovverider
        public virtual void PrintInfo()
        {
            Console.WriteLine("============================");
            Console.WriteLine("-------- Data Tiket --------");
            Console.WriteLine($"\nName\t: {Name}");
            Console.WriteLine("----------------------------");
        }

        
        //deklarasi fungsi abstract
        public abstract int Discount();

    }
}
