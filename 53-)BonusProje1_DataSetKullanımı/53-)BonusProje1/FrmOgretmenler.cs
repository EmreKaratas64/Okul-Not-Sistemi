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

namespace _53__BonusProje1
{
    public partial class FrmOgretmenler : Form
    {
        public FrmOgretmenler()
        {
            InitializeComponent();
        }

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();

        SqlConnection baglanti = new SqlConnection(@"Data Source=Z580;Initial Catalog=BonusOkul;Integrated Security=True");

        private void FrmOgretmenler_Load(object sender, EventArgs e)
        {
            // bu işlemi yapıyorsan ekleme işlemnde combobox için text değil selectedvalue yaz !!
            dataGridView1.DataSource = ds.OgretmenListele();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDESLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbBrans.DisplayMember = "DERSAD";
            CmbBrans.ValueMember = "DERSID";
            CmbBrans.DataSource = dt;
            baglanti.Close();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgretmenListele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            ds.OgretmenEkle(byte.Parse(CmbBrans.SelectedValue.ToString()), TxtAdSoyad.Text); // burada cmbbrans için selected value demek önemli yoksa giriş dizesi doğru değildi hatası alırsın !
            MessageBox.Show("Ekleme işlemi başarıyla gerçekleştirilmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.OgretmenListele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgretmenSil(byte.Parse(TxtID.Text.ToString()));
            MessageBox.Show("Silme işlemi başarıyla gerçekleştirilmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.OgretmenListele();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgretmenGetir(TxtBul.Text);
        }

        private void TxtAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT OGRTID,DERSAD,OGRTADSOYAD FROM TBLOGRETMENLER INNER JOIN TBLDESLER ON TBLOGRETMENLER.OGRTBRANS=TBLDESLER.DERSID WHERE OGRTADSOYAD like '" + TxtAra.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Black;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }
    }
}
// Select DERSAD,SINAV1,SINAV2,SINAV3,PROJE,ORTALAMA,DURUM FROM TBLNOTLAR INNER JOIN TBLDESLER ON TBLNOTLAR.DERSID = TBLDESLER.DERSID WHERE OGRID = @P1
// KitapAd like '" + textBox1.Text + "%'