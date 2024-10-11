// Folder: /Program.cs
using System;
using ConsoleApp1.helper;
using ConsoleApp1.model;

namespace RestaurantInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper dbHelper = new Helper();
            dbHelper.CreateTable();  
            int choice;
            do
            {
                Console.WriteLine("\nMenu Manajemen Inventaris Restoran");
                Console.WriteLine("1. Tambah Bahan");
                Console.WriteLine("2. Lihat Semua Bahan");
                Console.WriteLine("3. Perbarui Bahan");
                Console.WriteLine("4. Hapus Bahan");
                Console.WriteLine("5. Cek Bahan yang Akan Kadaluarsa");
                Console.WriteLine("6. Keluar");
                Console.Write("Pilih opsi: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Nama: ");
                        string nama = Console.ReadLine()!;
                        Console.Write("Jenis: ");
                        string jenis = Console.ReadLine()!;
                        Console.Write("Kuantitas: ");
                        int kuantitas = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Tanggal Kadaluarsa (YYYY-MM-DD): ");
                        DateTime tanggalKadaluarsa = DateTime.Parse(Console.ReadLine()!);

                        Bahan bahanBaru = new Bahan(nama, jenis, kuantitas, tanggalKadaluarsa);
                        dbHelper.AddBahan(bahanBaru);
                        break;

                    case 2:
                        dbHelper.ViewBahan();
                        break;

                    case 3:
                        Console.Write("ID Bahan yang Ingin Diperbarui: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Nama Baru: ");
                        string updateNama = Console.ReadLine()!;
                        Console.Write("Kuantitas Baru: ");
                        int updateKuantitas = Convert.ToInt32(Console.ReadLine());
                        dbHelper.UpdateBahan(updateId, updateNama, updateKuantitas);
                        break;

                    case 4:
                        Console.Write("ID Bahan yang Ingin Dihapus: ");
                        int deleteId = Convert.ToInt32(Console.ReadLine());
                        dbHelper.DeleteBahan(deleteId);
                        break;

                    case 5:
                        dbHelper.CheckExpiringSoon();
                        break;

                    case 6:
                        Console.WriteLine("Keluar dari program.");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            } while (choice != 6);
        }
    }
}
