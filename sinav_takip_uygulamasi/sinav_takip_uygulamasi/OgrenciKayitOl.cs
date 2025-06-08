using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace sinav_takip_uygulamasi
{
    public partial class OgrenciKayitOl : Form
    {
        SinavTakipSinifi sinavTakip = new SinavTakipSinifi();
        public OgrenciKayitOl()
        {
            InitializeComponent();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {

            string numara = txtNumara.Text.Trim();
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            string sifre = txtSifre.Text;

            if (numara == "" || ad == "" || soyad == "" || sifre == "")
            {
                MessageBox.Show("Tüm alanları doldurunuz.");
                return;
            }

            try
            {
                using (SqlConnection conn = sinavTakip.Baglanti())
                {
                    conn.Open();

                    // Önce aynı numara var mı kontrol et
                    string kontrolSql = "SELECT COUNT(*) FROM Ogrenciler WHERE Numara = @Numara";
                    SqlCommand kontrolCmd = new SqlCommand(kontrolSql, conn);
                    kontrolCmd.Parameters.AddWithValue("@Numara", numara);
                    int sayi = (int)kontrolCmd.ExecuteScalar();

                    if (sayi > 0)
                    {
                        MessageBox.Show("Bu numara ile kayıtlı bir öğrenci zaten mevcut.");
                        return;
                    }

                    // Kayıt işlemi
                    string sql = "INSERT INTO Ogrenciler (Numara, Ad, Soyad, Sifre) VALUES (@Numara, @Ad, @Soyad, @Sifre)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Numara", numara);
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Sifre", sifre);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Kayıt başarılı.");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation
                {
                    MessageBox.Show("Bu numara zaten sistemde kayıtlı.");
                }
                else
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }


        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            // Yapılacak işlemler buraya
        }


    }
}

