using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ukk_app
{
    internal class db
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=zeamart";
            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("My Sql Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        public static void AddItem(Item item)
        {
            string sql = "INSERT INTO item VALUES(NULL, @Produk, @Kode, @Harga, @Stok, @Gambar)";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@Produk", MySqlDbType.VarChar).Value = item.Produk;
            cmd.Parameters.Add("@Kode", MySqlDbType.VarChar).Value = item.Kode;
            cmd.Parameters.Add("@Harga", MySqlDbType.VarChar).Value = item.Harga;
            cmd.Parameters.Add("@Stok", MySqlDbType.VarChar).Value = item.Stok;
            cmd.Parameters.Add("@Gambar", MySqlDbType.Blob).Value = item.Gambar;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Gagal Menambahkan \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        public static void UpdateItem(Item item, string id)
        {
            string sql = "UPDATE item SET Produk=@Produk, Kode=@Kode, Harga=@Harga, Stok=@Stok, Img=@Gambar WHERE ID=@Id";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@Produk", MySqlDbType.VarChar).Value = item.Produk;
            cmd.Parameters.Add("@Kode", MySqlDbType.VarChar).Value = item.Kode;
            cmd.Parameters.Add("@Harga", MySqlDbType.VarChar).Value = item.Harga;
            cmd.Parameters.Add("@Stok", MySqlDbType.VarChar).Value = item.Stok;
            cmd.Parameters.Add("@Gambar", MySqlDbType.Blob).Value = item.Gambar;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Gagal Mengubah \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        public static void DeleteItem(string id)
        {
            string sql = "DELETE FROM item WHERE ID = @Id";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Gagal Menghapus \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        public static void DisplayAndSearch(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable(sql);
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            conn.Close();
        }
    }
}
