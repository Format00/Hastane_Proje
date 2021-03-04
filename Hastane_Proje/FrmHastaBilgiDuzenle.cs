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
    public partial class FrmHastaBilgiDuzenle : Form
    {
        public FrmHastaBilgiDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglantısı ss = new SqlBaglantısı();

        public string TCno;
        private void FrmHastaBilgiDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select HastaAd From Tbl_Hastalar", ss.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[0].ToString();
            }
            SqlCommand komut2 = new SqlCommand("Select HastaSoyad From Tbl_Hastalar", ss.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                TxtSoyad.Text = dr2[0].ToString();
            }
            SqlCommand komut3 = new SqlCommand("Select HastaTelefon From Tbl_Hastalar", ss.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                MskTel.Text = dr3[0].ToString();
            }
            SqlCommand komut4 = new SqlCommand("Select HastaSifre From Tbl_Hastalar", ss.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                TxtSifre.Text = dr4[0].ToString();
            }
            SqlCommand komut5 = new SqlCommand("Select HastaCinsiyet From Tbl_Hastalar", ss.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                CmbCinsiyet.Text = dr5[0].ToString();
            }

            MskTcNo.Text = TCno;
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTc=@p6", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel.Text);
            komut.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
            komut.Parameters.AddWithValue("@p6", MskTcNo.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Bilgileriniz Başarı İle Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
