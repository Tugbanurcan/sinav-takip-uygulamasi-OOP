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
    public partial class OgretmenSayfasi : Form
    {
        private SinavTakipSinifi sinavtakip;
       
        public OgretmenSayfasi()
        {
            InitializeComponent();
            sinavtakip = new SinavTakipSinifi();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz öğrenciyi seçin.");
                return;
            }

            // Seçili öğrencinin numarasını alalım
            string numara = dataGridView1.SelectedRows[0].Cells["Numara"].Value.ToString();

            // ÖğrenciID'yi alalım
            int ogrenciId = -1;
            using (SqlConnection con = sinavtakip.Baglanti())
            {
                con.Open();
                string sorgu = "SELECT OgrenciID FROM Ogrenciler WHERE Numara = @numara";
                using (SqlCommand cmd = new SqlCommand(sorgu, con))
                {
                    cmd.Parameters.AddWithValue("@numara", numara);
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Öğrenci bulunamadı.");
                        return;
                    }
                    ogrenciId = Convert.ToInt32(result);
                }
            }

            // Silme işleminden önce onay alalım
            DialogResult dr = MessageBox.Show($"{numara} numaralı öğrenciyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                sinavtakip.OgrenciSil(ogrenciId);
                MessageBox.Show("Öğrenci başarıyla silindi.");
                sinavtakip.OgrenciNotlariniYukle(dataGridView1);
            }
        }

        private void ogrenciSil_Click(object sender, EventArgs e)
        {
            
        }

        private void txtNumara_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void OgretmenSayfasi_Load(object sender, EventArgs e)
        {

            sinavtakip.OgrenciNotlariniYukle(dataGridView1); // DataGridView'e yükle
        }

        private void btnEksikNotlar_Click(object sender, EventArgs e)
        {
            SinavaGirmeyenler ogrci = new SinavaGirmeyenler();
            ogrci.Show();
            this.Hide();
        }

        private void btnNotGüncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz.");
                return;
            }

            string numara = dataGridView1.SelectedRows[0].Cells["Numara"].Value.ToString();

            // Önce öğrenciID'yi al
            int ogrenciId = -1;
            using (SqlConnection con = sinavtakip.Baglanti())
            {
                con.Open();
                string sorgu = "SELECT OgrenciID FROM Ogrenciler WHERE Numara = @numara";
                using (SqlCommand cmd = new SqlCommand(sorgu, con))
                {
                    cmd.Parameters.AddWithValue("@numara", numara);
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Öğrenci bulunamadı.");
                        return;
                    }
                    ogrenciId = Convert.ToInt32(result);
                }
            }

            int vize, final;
            int? vizeNot = int.TryParse(txtVize.Text, out vize) ? vize : (int?)null;
            int? finalNot = int.TryParse(txtFinal.Text, out final) ? final : (int?)null;

            if (!vizeNot.HasValue && !finalNot.HasValue)
            {
                MessageBox.Show("Lütfen güncellemek için en az bir not giriniz.");
                return;
            }

            sinavtakip.NotGuncelle(ogrenciId, vizeNot, finalNot);

            MessageBox.Show("Notlar başarıyla güncellendi.");
            sinavtakip.OgrenciNotlariniYukle(dataGridView1);
        }

        private void btnRaporla_Click(object sender, EventArgs e)
        {
            Rapor ogrci = new Rapor();
            ogrci.Show();
            this.Hide();

        }

        private void btnArsiv_Click(object sender, EventArgs e)
        {
            SilinenOgrenciler ogrci = new SilinenOgrenciler();
            ogrci.Show();
            this.Hide();
        }

        private void btnNotEkle_Click(object sender, EventArgs e)
        {
            // Önce DataGrid'den satır seçildi mi kontrol et
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz.");
                return;
            }

            // Seçilen satırdan numarayı al
            string numara = dataGridView1.SelectedRows[0].Cells["Numara"].Value.ToString();

            int vize, final;
            bool vizeGirildi = int.TryParse(txtVize.Text, out vize);
            bool finalGirildi = int.TryParse(txtFinal.Text, out final);

            if (!vizeGirildi && !finalGirildi)
            {
                MessageBox.Show("Lütfen en az bir not giriniz.");
                return;
            }

            using (SqlConnection con = sinavtakip.Baglanti())
            {
                con.Open();

                // Öğrenci ID'sini al
                string ogrenciSorgu = "SELECT OgrenciID FROM Ogrenciler WHERE Numara = @numara";
                SqlCommand cmdOgrenci = new SqlCommand(ogrenciSorgu, con);
                cmdOgrenci.Parameters.AddWithValue("@numara", numara);

                object result = cmdOgrenci.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("Seçilen öğrenci bulunamadı.");
                    return;
                }

                int ogrenciID = Convert.ToInt32(result);

                // Not tablosunda kayıt var mı?
                string kontrolSorgu = "SELECT COUNT(*) FROM Notlar WHERE OgrenciID = @id";
                SqlCommand cmdKontrol = new SqlCommand(kontrolSorgu, con);
                cmdKontrol.Parameters.AddWithValue("@id", ogrenciID);
                int sayi = (int)cmdKontrol.ExecuteScalar();

                if (sayi == 0)
                {
                    // INSERT işlemi
                    string ekleSorgu = "INSERT INTO Notlar (OgrenciID, Vize, Final) VALUES (@id, @vize, @final)";
                    SqlCommand cmdEkle = new SqlCommand(ekleSorgu, con);
                    cmdEkle.Parameters.AddWithValue("@id", ogrenciID);
                    cmdEkle.Parameters.AddWithValue("@vize", vizeGirildi ? (object)vize : DBNull.Value);
                    cmdEkle.Parameters.AddWithValue("@final", finalGirildi ? (object)final : DBNull.Value);
                    cmdEkle.ExecuteNonQuery();
                }
                else
                {
                    // UPDATE işlemi
                    string guncelleSorgu = "UPDATE Notlar SET ";
                    List<string> kolonlar = new List<string>();
                    if (vizeGirildi) kolonlar.Add("Vize = @vize");
                    if (finalGirildi) kolonlar.Add("Final = @final");

                    guncelleSorgu += string.Join(", ", kolonlar) + " WHERE OgrenciID = @id";

                    SqlCommand cmdGuncelle = new SqlCommand(guncelleSorgu, con);
                    if (vizeGirildi) cmdGuncelle.Parameters.AddWithValue("@vize", vize);
                    if (finalGirildi) cmdGuncelle.Parameters.AddWithValue("@final", final);
                    cmdGuncelle.Parameters.AddWithValue("@id", ogrenciID);
                    cmdGuncelle.ExecuteNonQuery();
                }

                MessageBox.Show("Not bilgisi başarıyla kaydedildi/güncellendi.");
                sinavtakip.OgrenciNotlariniYukle(dataGridView1); // tabloyu yenile
            }
        }


        private int GetOgrenciIdByNumara(string numara)
        {
            using (SqlConnection con = new SqlConnection(sinavtakip.connectionString))
            {
                string query = "SELECT OgrenciID FROM Ogrenciler WHERE Numara = @numara";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@numara", numara);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        private void txtogrAra_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnogrAra_Click(object sender, EventArgs e)
        {
            string aramaMetni = txtogrAra.Text.Trim();
            if (string.IsNullOrEmpty(aramaMetni))
            {
                // Arama boşsa tüm öğrencileri göster
                sinavtakip.OgrenciNotlariniYukle(dataGridView1);
            }
            else
            {
                sinavtakip.OgrenciAra(dataGridView1, aramaMetni);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
             
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {

            VizeFinalAyari ogrci = new VizeFinalAyari();
            ogrci.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
