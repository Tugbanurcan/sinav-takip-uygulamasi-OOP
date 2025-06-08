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
    public partial class VizeFinalAyari : Form
    {
        private SinavTakipSinifi sinavtakip;

        public VizeFinalAyari()
        {
            InitializeComponent();
            sinavtakip = new SinavTakipSinifi();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                int vizeOrani = int.Parse(bunifuMaterialTextbox1.Text);
                int finalOrani = int.Parse(bunifuMaterialTextbox2.Text);
                int gecmeNotu = int.Parse(bunifuMaterialTextbox3.Text);

                
                if (vizeOrani + finalOrani != 100)
                {
                    MessageBox.Show("Vize ve final oranlarının toplamı 100 olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                sinavtakip.AyarGuncelle("VizeOrani", vizeOrani.ToString());
                sinavtakip.AyarGuncelle("FinalOrani", finalOrani.ToString());
                sinavtakip.AyarGuncelle("GecmeNotu", gecmeNotu.ToString());

                sinavtakip.NotlarGuncelle();

                MessageBox.Show("Ayarlar ve notlar başarıyla güncellendi.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen tüm alanlara geçerli sayılar girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private void VizeFinalAyari_Load(object sender, EventArgs e)
        {
            var ayarlar = sinavtakip.AyarlariYukle();

            if (ayarlar.ContainsKey("VizeOrani"))
                bunifuMaterialTextbox1.Text = ayarlar["VizeOrani"].ToString();

            if (ayarlar.ContainsKey("FinalOrani"))
                bunifuMaterialTextbox2.Text = ayarlar["FinalOrani"].ToString();

            if (ayarlar.ContainsKey("GecmeNotu"))
                bunifuMaterialTextbox3.Text = ayarlar["GecmeNotu"].ToString();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            OgretmenSayfasi ogr = new OgretmenSayfasi();
            ogr.Show();
            this.Hide();
        }
    }
}
