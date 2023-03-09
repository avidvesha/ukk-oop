using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace ukk_app
{
    public partial class Form2 : Form
    {
        private readonly Form1 _parent;
        public string id, kode, produk, harga, stok;

        public Form2(Form1 parent)
        {
            InitializeComponent();
            _parent = parent;   
        }
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
        public void UpdateItem()
        {
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT img FROM item", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adp.Fill(table);

           /* byte[] img = (byte[])table.Rows[0][0];

            MemoryStream ms = new MemoryStream(img);

            pictureBox.Image = Image.FromStream(ms);

            adp.Dispose();*/

            /*adp.Fill(ds, "item");
            int c = ds.Tables["item"].Rows.Count;*/

            /*if (c > 0)
            {   //BLOB is read into Byte array, then used to construct MemoryStream,
                //then passed to PictureBox.
                Byte[] byteBLOBData = new Byte[0][5];
                byteBLOBData = (Byte[])(ds.Tables["item"].Rows[0][5]);
                MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                pictureBox.Image = Image.FromStream(stmBLOBData);
            }*/

            btnSave.Text = "Update";
            txtProduk.Text = produk;
            txtKode.Text = kode;
            txtHarga.Text = harga;
            txtStok.Text = stok;
        }

        public void Clear()
        {
            btnSave.Text = "Save";
            txtProduk.Text = txtKode.Text = txtHarga.Text = txtStok.Text = string.Empty;
            pictureBox.Image = null;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Choose Image | *.jpg;.png;.jpeg;.gif;";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = Image.FromFile(opf.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtProduk.Text.Trim().Length < 3)
            {
                MessageBox.Show("Nama produk kosong ( > 3 ).");
                return;
            }
            if (txtKode.Text.Trim().Length < 3)
            {
                MessageBox.Show("Kode produk kosong ( > 3 ).");
                return;
            }
            if (txtHarga.Text == null)
            {
                MessageBox.Show("Harga produk kosong ( > 3 ).");
                return;
            }
            if (txtStok.Text == null)
            {
                MessageBox.Show("Stok produk kosong ( > 1 ).");
                return;
            }
            if (pictureBox.Text.Length > 0 )
            {
                MessageBox.Show("Gambar produk kosong ( > 1 ).");
                return;
            }
            if (btnSave.Text == "Save")
            {
                MemoryStream ms = new MemoryStream();
                pictureBox.Image.Save(ms, pictureBox.Image.RawFormat);
                byte[] img = ms.ToArray();

                string sql = "INSERT INTO item VALUES(NULL, @Kode, @Produk, @Harga, @Stok, @Gambar)";
                MySqlConnection conn = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@Produk", MySqlDbType.VarChar).Value = txtProduk.Text;
                cmd.Parameters.Add("@Kode", MySqlDbType.VarChar).Value = txtKode.Text;
                cmd.Parameters.Add("@Harga", MySqlDbType.VarChar).Value = txtHarga.Text;
                cmd.Parameters.Add("@Stok", MySqlDbType.VarChar).Value = txtStok.Text;
                cmd.Parameters.Add("@Gambar", MySqlDbType.Blob).Value = img;

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Gagal Menambahkan \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            if(btnSave.Text == "Update")
            {
                string sql = "UPDATE item SET  Kode=@Kode, Produk=@Produk, Harga=@Harga, Stok=@Stok, Img=@Gambar WHERE ID=@Id";
                MySqlConnection conn = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@Kode", MySqlDbType.VarChar).Value = txtKode.Text;
                cmd.Parameters.Add("@Produk", MySqlDbType.VarChar).Value = txtProduk.Text;
                cmd.Parameters.Add("@Harga", MySqlDbType.VarChar).Value = txtHarga.Text;
                cmd.Parameters.Add("@Stok", MySqlDbType.VarChar).Value = txtStok.Text;

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil Diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ = btnSave.Text == "Save";

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Gagal Mengubah \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            _parent.Display();
        }

       
    }
}
