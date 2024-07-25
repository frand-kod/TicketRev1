using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx;
using Mysqlx.Crud;

namespace TicketRev1
{
    internal class Program
    {
        public static void Main()
        {
            // Deklarasi collection untuk menampung tiket 
            List<TiketBisnis> TicBisnis = new List<TiketBisnis>();
            List<TiketEkonomi> TicEkonomi = new List<TiketEkonomi>();

            // deklarasi database untuk penyimpanan data
            string connStr = "server=localhost;user=root;database=dbTicket;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);


            while (true)
            {
                Console.Clear(); // Bersihkan layar sebelum menampilkan menu
                Console.WriteLine("----- ++  Aplikasi Tiket Pesawat  ++ -----");
                Console.WriteLine("===========================================");

                //print harga terkini

                TiketBisnis Tiket1 = new TiketBisnis(null, null);
                Tiket1.PrintPrice();
                TiketEkonomi Tiket2 = new TiketEkonomi(null, null, false);
                Tiket2.PrintPrice();

                Console.WriteLine("-------------------------------------------");

                Console.WriteLine("Pilih Ingin Membuat Jenis Tiket Apa : ");
                Console.WriteLine("1. Bisnis \n2. Ekonomi \n3. Lihat Rincian Tiket \n4. Simpan Ke database\n5. Lihat Data yang tersimpan\n6. Hapus Semua data di database\n\n99. Keluar");
                Console.WriteLine("\n-------------------------------------------");

                Console.Write("Pilihan Anda (1 .. 5) : ");
                //validasi hanya inputan 1 sd 4 yang diizinkan
                bool validInput = int.TryParse(Console.ReadLine(), out int pil);

                if (!validInput || pil < 1 || pil > 6 && pil != 99)
                {
                    Console.WriteLine("Masukkan inputan yang benar antara 1 hingga 6.");
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
                            string valName = Console.ReadLine();

                            Console.Write("Alamat : ");
                            string valAlamat = Console.ReadLine();

                            // Masukkan data ke constructor TiketBisnis
                            TiketBisnis TiketBisnis = new TiketBisnis(valName, valAlamat);

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
                            string valNameEko = Console.ReadLine();

                            Console.Write("Alamat : ");
                            string valAlamatEko = Console.ReadLine();

                            //konfirmasi apakah ingin di diskon?
                            Console.Write("Ingin Didiskon ? (y/n) :");
                            char phil = Convert.ToChar(Console.ReadLine());

                            // Masukkan data ke constructor TiketEkonomi dengan diskon/tidak
                            TiketEkonomi TiketEkonomi = new TiketEkonomi(valNameEko, valAlamatEko, Utils.IsDiscountable(phil)); ;

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

                            Console.WriteLine("\n------- +++++ -------\n");

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
                            Console.WriteLine("Simpan ke Database");
                            Utils.PrintDots(6, "load");

                            try
                            {
                                conn.Open();
                                Console.WriteLine("Terhubung ke database");


                                if (TicEkonomi.Count == 0 && TicBisnis.Count == 0)
                                {
                                    Console.WriteLine("Tidak ada tiket untuk disimpan.");
                                }
                                else
                                {

                                    Utils.PrintDots(5, "save");
                                    foreach (var tiket in TicEkonomi)
                                    {
                                        string checkQuery = "select count(*) from tiket_ekonomi where id = @id";
                                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                                        checkCmd.Parameters.AddWithValue("@id", "Eko-" + tiket.Id);

                                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                        if (count == 0)
                                        {
                                            string sqlInsert = "insert tiket_ekonomi (id, nama, harga, alamat) values (@id, @nama, @harga, @alamat)";
                                            MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);

                                            cmd.Parameters.AddWithValue("@id", "Eko-" + tiket.Id);
                                            cmd.Parameters.AddWithValue("@nama", tiket.Name);
                                            cmd.Parameters.AddWithValue("@harga", tiket.Price);
                                            cmd.Parameters.AddWithValue("@alamat", tiket.Alamat);

                                            int rowCount = cmd.ExecuteNonQuery();
                                            if (rowCount > 0)
                                            {
                                                Console.WriteLine("Tiket Ekonomi, {0} tersimpan", tiket.Name);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Tidak ada tiket Ekonomi yang tersimpan");
                                            }
                                        }
                                        else
                                        {

                                            Console.WriteLine("Tiket Ekonomi dengan ID {0} dan Nama {1} sudah ada.", "Eko-" + tiket.Id, tiket.Name);
                                        }
                                    }

                                    foreach (var tiket in TicBisnis)
                                    {
                                        string checkQuery = "select count(*) from tiket_bisnis where id = @id";
                                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                                        checkCmd.Parameters.AddWithValue("@id", "Bis-" + tiket.Id);

                                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                        if (count == 0)
                                        {
                                            string sqlInsert = "insert tiket_bisnis (id, nama, harga, alamat) values (@id, @nama, @harga, @alamat)";
                                            MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);

                                            cmd.Parameters.AddWithValue("@id", "Bis-" + tiket.Id);
                                            cmd.Parameters.AddWithValue("@nama", tiket.Name);
                                            cmd.Parameters.AddWithValue("@harga", tiket.Price);
                                            cmd.Parameters.AddWithValue("@alamat", tiket.Alamat);

                                            int rowCount = cmd.ExecuteNonQuery();
                                            if (rowCount > 0)
                                            {
                                                Console.WriteLine("Tiket Bisnis, {0} tersimpan", tiket.Name);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Tidak ada tiket Bisnis yang tersimpan");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Tiket Bisnis dengan ID {0} dan Nama {1} sudah ada.", "Bis-" + tiket.Id, tiket.Name);
                                        }
                                    }
                                }
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.ToString());
                            }
                            finally
                            {
                                conn.Close();
                            }

                            Console.ReadKey();
                            break;


                        case 5:
                            Utils.PrintDots(5, "read");
                            Console.WriteLine("List Data yg Tersimpan di database");
                            conn.Open();//buka akses ke database

                            try
                            {
                                //mulai pembacaan tiket bisnis
                                //query untuk membaca data 
                                string sqlReadBis = "select * from tiket_bisnis";

                                //deklarasi method comaandSql
                                MySqlCommand cmdBis = new MySqlCommand(sqlReadBis, conn);

                                //deklarasi method readerSql
                                MySqlDataReader readerBis = cmdBis.ExecuteReader();

                                Console.WriteLine("\nData Tiket Bisnis\n");
                                int noBis = 1; // untuk nomer secara urut
                                while (readerBis.Read())
                                {
                                    //tampilkan sebaris demi baris 
                                    Console.WriteLine(noBis + ". Id tiket\t: {0},\tNama\t: {1},\tAlamat\t: {2},\tHarga\t: {3}", readerBis["id"], readerBis["nama"], readerBis["alamat"], readerBis["harga"]);
                                    noBis++;
                                }
                                readerBis.Close();


                                //Mulai pembacaan dari db tiketekonomi
                                string sqlReadEko = "select * from tiket_ekonomi";
                                MySqlCommand cmdEko = new MySqlCommand(sqlReadEko, conn);
                                MySqlDataReader readerEko = cmdEko.ExecuteReader();

                                Console.WriteLine("\nData Tiket Ekonomi\n");
                                int noEko = 1;
                                while (readerEko.Read())
                                {
                                    Console.WriteLine(noEko + ". Id tiket\t: {0},\tNama\t: {1},\tAlamat\t: {2},\tHarga\t: {3}", readerEko["id"], readerEko["nama"], readerEko["alamat"], readerEko["harga"]);
                                    noEko++;

                                }
                                readerEko.Close();
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.ToString());
                                Console.WriteLine("\ntekan sembarang tombol untuk kembali ke menu utama");

                                Console.ReadKey();

                            }
                            finally
                            {
                                conn.Close();
                                Console.WriteLine("\ntekan sembarang tombol untuk kembali ke menu utama");
                                Console.ReadKey();
                            }
                            break;
                        case 6:
                            Utils.PrintDots(5, "load");
                            Console.Write("Hapus Semua data pada database\n\n Apakah Anda Yakin? (y/n) : ");
                            string pill = Console.ReadLine();

                            if (pill == "y")
                            {
                                try
                                {
                                    conn.Open();
                                    //buat query penghapusan
                                    string sqlDelBis = "delete from tiket_bisnis";
                                    string sqlDelEko = "delete from tiket_ekonomi";

                                    //Panggil method mysql
                                    MySqlCommand cmdDelBis = new MySqlCommand(sqlDelBis, conn);
                                    MySqlCommand cmdDelEko = new MySqlCommand(sqlDelEko, conn);

                                    //jalankan perintah dan ambil jumlah baris yang terhapus (optionl)
                                    int countDelBis = cmdDelBis.ExecuteNonQuery();
                                    int countDelEko = cmdDelEko.ExecuteNonQuery();

                                    //tampilkan informasi penghapusan
                                    if (countDelBis > 0 || countDelEko > 0)
                                    {

                                        Console.WriteLine("Data Sukses terhapus !! ");
                                        Console.WriteLine("Jumlah terhapus \n - Tiket Bisnis\t\t: {0}\n - Tiket Ekonomi\t: {1}", countDelBis, countDelEko);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Tidak ada Data di database \n tekan semabaran tombol untuk kembali");
                                    }
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine(err.ToString());
                                    Console.ReadKey();
                                }
                                finally
                                {
                                    conn.Close();
                                    Console.ReadKey();

                                }
                            }
                            else if (pill == "n")
                            {
                                Console.WriteLine("Okay.. data batal di hapus\ntekan sembarang tombol untuk kembali ke menu utama");
                                Console.ReadKey();

                            }
                            else
                            {
                                Console.WriteLine("Hanya masukkan y/n, sembarang tombol untuk ke menu utama");
                                Console.ReadKey();
                            }
                            break;
                        case 99:
                            Console.WriteLine("Keluar...");
                            Console.ReadKey();

                            break;
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