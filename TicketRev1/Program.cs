using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicketRev1
{
    internal class Program
    {
        static void Main()
        {
            // Deklarasi collection untuk menampung tiket bisnis
            List<TiketBisnis> TicBisnis = new List<TiketBisnis>();
            List<TiketEkonomi>TicEkonomi = new List<TiketEkonomi>();


            while (true)
            {
                Console.Clear(); // Bersihkan layar sebelum menampilkan menu
                Console.WriteLine("--- ++ Aplikasi Register Tiket Pesawat++ ---");
                Console.WriteLine("===========================================");

                //print harga terkini

                TiketBisnis Tiket1 = new TiketBisnis(null);
                Tiket1.PrintPrice();
                TiketEkonomi Tiket2 = new TiketEkonomi(null,false);
                Tiket2.PrintPrice();

                Console.WriteLine("===========================================");

                Console.WriteLine("Pilih Ingin Membuat Jenis Tiket Apa : ");
                Console.WriteLine("1. Bisnis \n2. Ekonomi \n3. Lihat Rincian Tiket \n4. Keluar");

                Console.Write("Pilihan Anda (1 .. 4) : ");
                //validasi hanya inputan 1 sd 5 yang diizinkan
                bool validInput = int.TryParse(Console.ReadLine(), out int pil);

                if (!validInput || pil < 1 || pil > 4)
                {
                    Console.WriteLine("Masukkan inputan yang benar antara 1 hingga 5.");
                    Console.ReadKey();
                    continue; // Kembali ke awal loop jika input tidak valid
                }
                else
                {
                    switch (pil)
                    {
                        case 1:
                            Utils.PrintDots(5, "load");
                            Console.WriteLine("Tiket Bisnis\n=============\n\n+ Masukkan Data Anda \n");
                            Console.Write("Nama : ");
                            string ValName = Console.ReadLine();

                            // Masukkan data ke constructor TiketBisnis
                            TiketBisnis TiketBisnis = new TiketBisnis(ValName);


                            // Creating data animation
                            Utils.PrintDots(5, "create");



                            // Masukkan ke list object bertipe 'TiketBisnis'
                            TicBisnis.Add(TiketBisnis);

                            // Tampilkan informasi dari tiket bisnis
                            Console.WriteLine("Data Sukses terInput ..\nBerikut data Tiket Bisnis Anda : \n");
                            TiketBisnis.PrintInfo();
                            Console.ReadKey();

                            break;
                        case 2:

                            Utils.PrintDots(5, "load");
                            Console.WriteLine("Tiket Ekonomi\n=============\n\n+ Masukkan Data Anda \n");
                            Console.Write("Nama : ");
                            string ValNameEko = Console.ReadLine();

                            //konfirmasi apakah ingin di diskon?
                            Console.Write("Ingin Didiskon ? (y/n) :");
                            char phil = Convert.ToChar(Console.ReadLine());

                            // Masukkan data ke constructor TiketEkonomi dengan diskon/tidak
                            TiketEkonomi TiketEkonomi = new TiketEkonomi(ValNameEko,Utils.IsDiscountable(phil));

                            // Creating data animation
                            Utils.PrintDots(5, "create");

                            // Masukkan ke list object bertipe 'TiketEkonomi'
                            TicEkonomi.Add(TiketEkonomi);

                            // Tampilkan informasi dari TiketEkonomi
                            Console.WriteLine("Data Sukses terInput ..\nBerikut data Tiket Ekonomi Anda : \n");
                            TiketEkonomi.PrintInfo();
                            Console.ReadKey();
                            break;
                        case 3:
                            Utils.PrintDots(5, "load");
                            Console.Clear();

                            if (TicBisnis.Count > 0)
                            {
                                Console.WriteLine("Berikut Rincian Tiket Yang telah terinput");
                                Console.WriteLine(" ===  Tiket Bisnis ===");
                                foreach (var tiket in TicBisnis)
                                {
                                    tiket.PrintInfo();
                                }
                            }
                            if (TicEkonomi.Count > 0)
                            {
                                Console.WriteLine(" === Tiket Ekonomi === ");
                                foreach (var tiket in TicEkonomi)
                                {
                                    tiket.PrintInfo();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Tidak ada tiket yang terdaftar.");
                            }
                            //kembali ke menu
                            Console.WriteLine("Enter untuk kembali ke menu utama ..");
                            Console.ReadKey();
                            break;

                        case 4:
                            Console.WriteLine("Keluar...");
                            Console.ReadKey();
                            return;
                        default:
                            Console.WriteLine("Salah Input !");
                            Console.ReadKey();
                            break;
                    
                    }
                }

            }
        }
    }
}
