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
    public partial class SinavaGirmeyenler : Form
    {
        SinavTakipSinifi sinavTakip = new SinavTakipSinifi();
        public SinavaGirmeyenler()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            sinavTakip.VizeyeGirmeyenler(dataGridView1);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            sinavTakip.FinaleGirmeyenler(dataGridView1);

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            sinavTakip.HicSinavaGirmeyenler(dataGridView1);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            OgretmenSayfasi tablo_sayfasi = new OgretmenSayfasi();
            tablo_sayfasi.Show();
            this.Hide();
        }
    }
}
