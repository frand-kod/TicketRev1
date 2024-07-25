using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicketRev1
{
    public class Utils
    {
        //buat ada titik2 berjalan
        public static void PrintDots(int CountOfDots, string typeMsg)
        {
            if (typeMsg == "save")
            {
                Console.WriteLine("Saving Data, Please Wait ");

            }
            else if (typeMsg == "load")
            {
                Console.WriteLine("Loading Data, Please Wait ");
            }
            else if (typeMsg == "create")
            {
                Console.WriteLine("Creating Data, Please Wait ");
            }
            else if (typeMsg == null)
            {
                Console.Write("");
            }
            else if(typeMsg == "read")
            {
                Console.Write("Reading Data, Please Wait");
            }

            for (int i = 0; i < CountOfDots; i++)
            {
                Console.Write(". ");
                Thread.Sleep(100); //delay 300 ms per titik
            }
            Console.Clear();

        }

        //buat readonly id random dan generate
        private static readonly Random Random = new Random();
        public static int GenerateRandomId()
        {
            return Random.Next(1000000, 9999999); // ID acak antara 100000 dan 999999
        }
        
        //cek apakah bisa mendapatkan diskon
        public static bool IsDiscountable(char input)
        {
            if (input == 'y')
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
