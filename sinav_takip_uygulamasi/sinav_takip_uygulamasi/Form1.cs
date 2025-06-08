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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            OgretmenAdminPaneli ogrtmenAdminSayfasi = new  OgretmenAdminPaneli();
            ogrtmenAdminSayfasi.Show();
            this.Hide();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            OgrenciAdminPaneli ogrPaneliSayfasi = new OgrenciAdminPaneli();
            ogrPaneliSayfasi.Show();
            this.Hide();

        }
    }
}
