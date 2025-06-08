using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinav_takip_uygulamasi
{
    public partial class SilinenOgrenciler : Form
    {
        SinavTakipSinifi sinav = new SinavTakipSinifi();
        public SilinenOgrenciler()
        {
            InitializeComponent();
        }

        private void SilinenOgrenciler_Load(object sender, EventArgs e)
        {
            sinav.ArsivOgrencileriGetir(dataGridView1);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            OgretmenSayfasi tablo_sayfasi = new OgretmenSayfasi();
            tablo_sayfasi.Show();
            this.Hide();
        }
    }
}
