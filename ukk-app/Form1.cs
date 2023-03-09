using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ukk_app
{
    public partial class Form1 : Form
    {

        Form2 form;

        public Form1()
        {
            InitializeComponent();
            form = new Form2(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            form.Clear();
            form.ShowDialog();
        }

        public void Display()
        {
            db.DisplayAndSearch("SELECT id, kode, produk, harga, stok, img FROM item", dataGridView);
        }


        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM item WHERE CONCAT(id, kode, produk, harga, stok) LIKE'%"+textBox1.Text+"%'", dataGridView);

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                form.Clear();
                form.id = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.kode = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.produk = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.harga = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.stok = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.UpdateItem();
                form.ShowDialog();

                return;
            }
            if(e.ColumnIndex == 1)
            {
                if(MessageBox.Show("Yakin mau dihapus?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    db.DeleteItem(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_Click(object sender, EventArgs e)
        {
            Byte[] img = (Byte[])dataGridView.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
        }
    }
}
