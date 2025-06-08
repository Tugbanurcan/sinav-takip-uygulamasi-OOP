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
    public partial class OgrenciGiris : Form
    {
        private SinavTakipSinifi sinavtakip;
        public OgrenciGiris()
        {
            InitializeComponent();
            sinavtakip = new SinavTakipSinifi();

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            /*string ogrenciNumara = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            int ogrenciID = sinavtakip.GetOgrenciID(ogrenciNumara, sifre);

            if (ogrenciID > 0)
            {
                OgrenciSayfasi ogrenciSayfa = new OgrenciSayfasi();
                ogrenciSayfa.OgrenciID = ogrenciID;
                ogrenciSayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Numara veya şifre hatalı!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/

            string ogrenciNumara = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            int ogrenciID = sinavtakip.GetOgrenciID(ogrenciNumara, sifre);

            if (ogrenciID > 0)
            {
                OgrenciSayfasi ogrenciSayfa = new OgrenciSayfasi();
                ogrenciSayfa.OgrenciID = ogrenciID;
                ogrenciSayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Numara veya şifre hatalı!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
