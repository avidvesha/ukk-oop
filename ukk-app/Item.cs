using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ukk_app
{
    internal class Item
    {
        public string Produk { get; set; }
        public string Kode{ get; set; }
        public string Harga{ get; set; }
        public string Stok { get; set; }
        public string Gambar { get; set; }

        public Item(string produk, string kode, string harga, string stok, string gambar)
        {
            Produk = produk;
            Kode = kode;
            Harga = harga;
            Stok = stok;
            Gambar = gambar;
        }
    }
}
