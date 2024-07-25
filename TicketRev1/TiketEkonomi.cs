using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRev1
{
    internal class TiketEkonomi : Tiket
    {
        private const string jenisTiket = "EKONOMI"; //deklarasi jenis tiket
        private readonly int priceEko = 15000;
        private readonly bool discount;

        private readonly string IdEko;


        public TiketEkonomi(string name, string alamat, bool discount) : base(name, alamat)
        {
            IdEko = "EKO-" + Id;
            base.Price = priceEko; //set price di class parent dengan price di class TicketEko
            this.discount = discount;
            this.Alamat = alamat;

        }
        //panggil method wajib dari abstract discount
        //karena ekonomi maka bisa pakai diskon
        public override int Discount()
        {
            int disc = priceEko * 90 / 100;// diskon 10%
            return disc;

        }

        public override void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.PrintInfo();//cetak informasi umum dari parent Tiket

            //cetak jenis tiket : Ekonomi

            Console.WriteLine("Id\t: {0}", IdEko);
            Console.WriteLine("Jenis\t: {0}", jenisTiket);

            if (discount)
            {
                Console.WriteLine("Harga\t: {0}", Discount());
            }
            else
            {
                Console.WriteLine("Harga\t: {0}", priceEko);
            }
            Console.WriteLine("============================");
            Console.ResetColor();

        }

        public void PrintPrice()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Harga Tiket Ekonomi: {0}", priceEko);
            Console.ResetColor();
        }
    }
}
