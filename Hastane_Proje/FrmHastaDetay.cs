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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        SqlBaglantısı ss = new SqlBaglantısı();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = tc;


            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTC=@p1", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] +" "+ dr[1];
            }
            ss.baglanti().Close();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_randevular where HastaTC =" +tc , ss.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;



            SqlCommand komut2 = new SqlCommand("Select BransAd from tbl_branslar", ss.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            ss.baglanti().Close();


            //SqlCommand komut3 = new SqlCommand("Select DoktorAd from Tbl_Doktorlar", ss.baglanti());
            //SqlDataReader dr3 = komut3.ExecuteReader();
            //while (dr3.Read())
            //{
            //    CmbDoktor.Items.Add(dr3[0]);
            //}
            //ss.baglanti().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from tbl_doktorlar where DoktorBrans=@p1", ss.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            ss.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where RandevuBrans ='" + CmbBrans.Text + "'" + "and RandevuDoktor='" + CmbDoktor.Text + "' and RandevuDurum=0", ss.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDuzenle fr = new FrmHastaBilgiDuzenle();
            fr.TCno = LblTc.Text;
            fr.Show();
            this.Hide();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tbl_randevular set RandevuDurum=1, HastaTC=@p1, HastaSikayet=@p2 where Randevuid=@p3", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Randevu Alındı");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
