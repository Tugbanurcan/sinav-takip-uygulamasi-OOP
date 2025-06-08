using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinav_takip_uygulamasi
{
    public partial class OgrenciAdminPaneli : Form
    {
        public OgrenciAdminPaneli()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            OgrenciGiris ogrci = new OgrenciGiris();
            ogrci.Show();
            this.Hide();

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            OgrenciKayitOl ogrci = new OgrenciKayitOl();
            ogrci.Show();
            this.Hide();


        }
    }



    
}
