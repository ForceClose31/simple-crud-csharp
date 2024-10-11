using System;

namespace ConsoleApp1.model
{
    public class Bahan{
        public int Id { get; set;} 
        public string Nama {get; set;}
        public string Jenis {get; set;} 
        public int Kuantitas {get; set;}
        public DateTime TanggalKadaluarsa {get; set;}
        public Bahan (string nama, string jenis, int kuantitas, DateTime tanggalKadaluarsa){
            Nama = nama;
            Jenis = jenis;
            Kuantitas = kuantitas;
            TanggalKadaluarsa = tanggalKadaluarsa;
        }

        public override string ToString()
        {
            return $"Id : {Id}, Nama : {Nama}, Jenis : {Jenis}, Kuantitas : {Kuantitas}, Tanggal Kadaluarsa : {TanggalKadaluarsa.ToShortDateString}";
        }
    };
}