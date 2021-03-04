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

namespace Hastane_Proje
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantısı ss = new SqlBaglantısı();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTcNo.Text = TCNO;

            SqlCommand komut = new SqlCommand("select * from tbl_doktorlar where doktortc=@p1", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTcNo.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[4].ToString();
            }
            ss.baglanti().Close();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_doktorlar set doktorad=@p1, doktorsoyad=@p2, doktorbrans=@p3, doktorsifre=@p4 where doktortc=@p5", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p5", MskTcNo.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi.");
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            FrmDoktorDetay fr = new FrmDoktorDetay();
            fr.Show();
            this.Hide();
        }
    }
}
