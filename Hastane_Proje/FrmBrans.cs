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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        SqlBaglantısı ss = new SqlBaglantısı();

        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar", ss.baglanti());
            dataGridView1.DataSource = dt;
            da.Fill(dt);
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_branslar (BransAd) values (@b1)", ss.baglanti());
            komut.Parameters.AddWithValue("@b1", TxtAd.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Branş Eklenmiştir.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From tbl_Branslar where Bransid=@p1", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Branş Silindi.");
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Branslar set BransAd=@p1 where Bransid=@p2", ss.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", Txtid.Text);
            komut.ExecuteNonQuery();
            ss.baglanti().Close();
            MessageBox.Show("Branş Güncellendi");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay fr = new FrmSekreterDetay();
            fr.Show();
            this.Hide();
        }
    }
}
