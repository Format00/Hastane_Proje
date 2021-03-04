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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        SqlBaglantısı ss = new SqlBaglantısı();

        public string TCnumara;
        
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = TCnumara;
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC=@p1", ss.baglanti());
            komut1.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            ss.baglanti().Close();


            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Bransid,BransAd from tbl_branslar", ss.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select DoktorAd+ ' '+DoktorSoyad as'Doktorlar', DoktorBrans from Tbl_Doktorlar", ss.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            SqlCommand komut2 = new SqlCommand("Select BransAd from tbl_branslar", ss.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            ss.baglanti().Close();

            
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@r1, @r2, @r3, @r4) ", ss.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.");

        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from tbl_doktorlar where DoktorBrans=@k1", ss.baglanti());
            komut3.Parameters.AddWithValue("@k1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            ss.baglanti().Close();
        }

        private void MskSaat_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_Duyurular (Duyuru) values (@d1)", ss.baglanti());
            komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
            this.Hide();
        }

        private void BranşPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
            this.Hide();
        }

        private void BtnRandeviList_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frr = new FrmRandevuListesi();
            frr.Show();
            this.Hide();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
            this.Hide();
        }
    }
}
