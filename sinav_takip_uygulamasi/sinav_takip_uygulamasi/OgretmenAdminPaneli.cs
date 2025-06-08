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
    public partial class OgretmenAdminPaneli : Form
    {
        public OgretmenAdminPaneli()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            SinavTakipSinifi sinavTakip = new SinavTakipSinifi();

            if (sinavTakip.OgretmenGirisKontrol(kullaniciAdi, sifre))
            {
                OgretmenSayfasi ogretmenSayfasi = new OgretmenSayfasi();
                ogretmenSayfasi.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
