using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kayitentity
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }
        KayıtSistemiEntities db = new KayıtSistemiEntities();

        private void button1_Click(object sender, EventArgs e)
        {
            string ders = Convert.ToString(textBox1.Text);
            Ders_Adlari da = new Ders_Adlari();
            da.DersAdi = ders;
            db.Ders_Adlari.Add(da);
            db.SaveChanges();
            MessageBox.Show("Kaydetme işlemi başarılı");
            txtremove();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox8.Text);
            var x = db.Ders_Adlari.Find(id);
            db.Ders_Adlari.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Silme işlemi başarılı");
            txtremove();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox8.Text);
            var x = db.Ders_Adlari.Find(id);
            x.DersAdi = Convert.ToString(textBox1.Text);
            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi başarılı");
            txtremove();
        }

        private void DersleriGoruntule_Click(object sender, EventArgs e)
        {
            var query = from item in db.Ders_Adlari
                        select new
                        {
                            id = item.id,
                            Ders=item.DersAdi
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void DersKodlarinigoruntule_Click(object sender, EventArgs e)
        {
            var query = from item in db.Ders_Secim
                        select new
                        {
                            id = item.id,
                            DersKodu=item.DersKodu,
                            Kredi=item.Kredi,
                            Saat=item.Saat,
                            idDers=item.idDers
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void HocalariGoruntule_Click(object sender, EventArgs e)
        {
            var query = from item in db.Hocalar1
                        select new
                        {
                            id=item.id,
                            Hoca=item.Hoca,
                            idHoca=item.idHoca
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void KodKaydet_Click(object sender, EventArgs e)
        {
            string ders_kodu = Convert.ToString(textBox2.Text);
            string kredi = Convert.ToString(textBox3.Text);
            string saat = Convert.ToString(textBox4.Text);
            int idDers= Convert.ToInt32(textBox5.Text);
            Ders_Secim da = new Ders_Secim();
            da.DersKodu = ders_kodu;
            da.Kredi = kredi;
            da.Saat = saat;
            da.idDers = idDers;
            db.Ders_Secim.Add(da);
            db.SaveChanges();
            MessageBox.Show("Kaydetme işlemi başarılı");
            txtremove();
        }

        private void KodSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox9.Text);
            var x = db.Ders_Secim.Find(id);
            db.Ders_Secim.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Silme işlemi başarılı");
            txtremove();
        }

        private void KodUp_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox9.Text);
            var x = db.Ders_Secim.Find(id);
            x.DersKodu = Convert.ToString(textBox2.Text);
            x.Kredi = Convert.ToString(textBox3.Text);
            x.Saat = Convert.ToString(textBox4.Text);
            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi başarılı");
            txtremove();
        }

        private void HocaKaydet_Click(object sender, EventArgs e)
        {
            string hoca = Convert.ToString(textBox7.Text);
            int idHoca= Convert.ToInt32(textBox6.Text);
            Hocalar1 da = new Hocalar1();
            da.Hoca = hoca;
            da.idHoca = idHoca;
            db.Hocalar1.Add(da);
            db.SaveChanges();
            MessageBox.Show("Kaydetme işlemi başarılı");
            txtremove();
        }

        private void HocaSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox10.Text);
            var x = db.Hocalar1.Find(id);
            db.Hocalar1.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Silme işlemi başarılı");
            txtremove();
        }

        private void HocaUp_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox10.Text);
            var x = db.Hocalar1.Find(id);
            x.Hoca = Convert.ToString(textBox7.Text);
            x.idHoca = Convert.ToInt32(textBox6.Text);
            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi başarılı");
            txtremove();
        }

        public void txtremove()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void admin_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
