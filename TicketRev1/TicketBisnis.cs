using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRev1
{
    internal class TiketBisnis : Tiket
    {
        private const string jenisTiket = "BISNIS"; //deklarasi jenis tiket
        private const int priceBis = 20000;

        private readonly string IdBisnis;


        public TiketBisnis(string name, string alamat) : base(name, alamat)
        {
            IdBisnis = "BIS-" + Id;
            this.Alamat = alamat;
            base.Price = priceBis; //set price di class parent dengan price di class TiketBisnis

        }
        //panggil method wajib dari abstract discount
        //karena bisnis maka tidak menerapkan diskon
        public override int Discount()
        {
            //karena tiket bisnis tidak menerapkan diskon
            throw new NotImplementedException();
        }
        public void PrintPrice()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Harga Tiket Bisnis: {0}", priceBis);
            Console.ResetColor();
        }

        public override void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.PrintInfo();//cetak informasi umum dari parent Tiket

            //cetak jenis tiket : BISNIS
            Console.WriteLine("Id\t: {0}", IdBisnis);
            Console.WriteLine("Jenis\t: {0}", jenisTiket);
            Console.WriteLine("Harga\t: {0}", priceBis);

            Console.WriteLine("============================");
            Console.ResetColor();

        }
        

        

    }
}
