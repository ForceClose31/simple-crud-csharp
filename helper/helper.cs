using Npgsql;
using ConsoleApp1.model;

namespace ConsoleApp1.helper
{
    public class Helper
    {
        private string connection = "Host=localhost;Username=postgres;Password=QWEASDZXC31;Database=sampledb";

        public void CreateTable()
        {
            using (var conn = new NpgsqlConnection(connection))
            {
                conn.Open();
                string createTableQuery = @"
                    DROP TABLE IF EXISTS bahan;
                    CREATE TABLE bahan (
                        id SERIAL PRIMARY KEY,
                        nama VARCHAR(100) NOT NULL,
                        jenis VARCHAR(50) NOT NULL,
                        kuantitas INTEGER NOT NULL,
                        tanggal_kadaluarsa DATE NOT NULL
                    );
                ";

                using (var cmd = new NpgsqlCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Tabel 'bahan' berhasil dibuat ulang.");
                }

            }
        }

        public void AddBahan(Bahan bahan){
            using (var conn = new NpgsqlConnection(connection)){
                conn.Open();
                string insertQuery = @"
                    INSERT INTO bahan (nama, jenis, kuantitas, tanggal_kadaluarsa)
                    VALUES (@nama, @jenis, @kuantitas, @tanggal_kadaluarsa);
                ";
                using (var cmd = new NpgsqlCommand(insertQuery, conn)){
                    cmd.Parameters.AddWithValue("nama", bahan.Nama);
                    cmd.Parameters.AddWithValue("jenis", bahan.Jenis);
                    cmd.Parameters.AddWithValue("kuantitas", bahan.Kuantitas);
                    cmd.Parameters.AddWithValue("tanggal_kadaluarsa", bahan.TanggalKadaluarsa);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data berhasil ditambahkan");

                }
            }
        }

        public void ViewBahan(){
            using (var conn = new NpgsqlConnection()){
                conn.Open();
                string selectQuery = @"SELECT * FROM bahan";

                using (var cmd = new NpgsqlCommand(selectQuery, conn)){
                    using (var reading = cmd.ExecuteReader()){
                        while (reading.Read()){
                            Console.WriteLine($"ID: {reading["id"]}, Nama: {reading["nama"]}, Jenis: {reading["jenis"]}, Kuantitas: {reading["kuantitas"]}, Tanggal Kadaluarsa: {((DateTime)reading["tanggal_kadaluarsa"]).ToShortDateString()}");
                        }
                    }
                }
            }
        }

        public void UpdateBahan(int id, string nama, int kuantitas)
        {
            using (var conn = new NpgsqlConnection(connection))
            {
                conn.Open();
                string updateQuery = "UPDATE bahan SET nama = @nama, kuantitas = @kuantitas WHERE id = @id";

                using (var cmd = new NpgsqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("nama", nama);
                    cmd.Parameters.AddWithValue("kuantitas", kuantitas);
                    cmd.Parameters.AddWithValue("id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Bahan berhasil diperbarui.");
                    else
                        Console.WriteLine("Bahan tidak ditemukan.");
                }
            }
        }

        public void DeleteBahan(int id)
        {
            using (var conn = new NpgsqlConnection(connection))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM bahan WHERE id = @id";

                using (var cmd = new NpgsqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Bahan berhasil dihapus.");
                    else
                        Console.WriteLine("Bahan tidak ditemukan.");
                }
            }
        }

        public void CheckExpiringSoon()
        {
            using (var conn = new NpgsqlConnection(connection))
            {
                conn.Open();
                string query = @"
                    SELECT * FROM bahan 
                    WHERE tanggal_kadaluarsa BETWEEN CURRENT_DATE AND CURRENT_DATE + INTERVAL '3 days'
                ";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool found = false;
                        while (reader.Read())
                        {
                            found = true;
                            Console.WriteLine($"Bahan yang akan kadaluarsa: {reader["nama"]} pada {((DateTime)reader["tanggal_kadaluarsa"]).ToShortDateString()}");
                        }

                        if (!found)
                        {
                            Console.WriteLine("Tidak ada bahan yang akan kadaluarsa dalam 3 hari ke depan.");
                        }
                    }
                }
            }
        }
    }
}
