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
    public partial class likeAdmin : Form
    {
        public likeAdmin()
        {
            InitializeComponent();
        }
        private BindingSource _source = new BindingSource();
        KayıtSistemiEntities db = new KayıtSistemiEntities();
        string tablo_ders;
        string tablo_hoca;
        string tablo_kod;

        private void button1_Click(object sender, EventArgs e)
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                string ders = comboBox1.Text;
                string dersKodu = Convert.ToString(comboBox2.Text);
                string kredi = Convert.ToString(ders_kredisi.Text);
                string saat = Convert.ToString(ders_saati.Text);
                string hoca = Convert.ToString(comboBox3.Text);
                List<Ders_Adlari> id = db.Ders_Adlari.Where(x => x.DersAdi == ders).ToList();

                bool durum1 = db.Ders_Adlari.Any(x => x.DersAdi == ders);
                bool durum2 = db.Ders_Secim.Where(x => x.DersKodu == dersKodu).Any();
                bool durum3 = db.Hocalar1.Where(x => x.Hoca == hoca).Any();

                Ders_Adlari da = new Ders_Adlari();
                Ders_Secim ds = new Ders_Secim();
                Hocalar1 h = new Hocalar1();


                if (!durum1 && !durum2 && !durum3)
                {
                    da.DersAdi = ders;
                    db.Ders_Adlari.Add(da);
                    db.SaveChanges();

                    ds.DersKodu = dersKodu;
                    ds.Kredi = kredi;
                    ds.Saat = saat;
                    ds.idDers = da.id;
                    db.Ders_Secim.Add(ds);
                    db.SaveChanges();

                    h.Hoca = hoca;
                    h.idHoca = da.id;
                    db.Hocalar1.Add(h);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 1");
                }
                else if (durum1 && !durum2 && !durum3)
                {
                    string realid = id[0].id.ToString();
                    ds.DersKodu = dersKodu;
                    ds.Kredi = kredi;
                    ds.Saat = saat;
                    ds.idDers = Convert.ToInt32(realid);
                    db.Ders_Secim.Add(ds);
                    db.SaveChanges();

                    h.Hoca = hoca;
                    h.idHoca = Convert.ToInt32(realid);
                    db.Hocalar1.Add(h);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 2");
                }
                else if (durum1 && durum2 && !durum3)
                {
                    string realid = id[0].id.ToString();
                    h.Hoca = hoca;
                    h.idHoca = Convert.ToInt32(realid);
                    db.Hocalar1.Add(h);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 3");
                }
                else if (durum1 && durum2 && durum3)
                {
                    MessageBox.Show("Bu bilgilere sahip ders bulunmaktadır");
                }
                else if (!durum1 && durum2 && durum3)
                {
                    da.DersAdi = ders;
                    db.Ders_Adlari.Add(da);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 5");
                }
                else if (!durum1 && durum2 && !durum3)
                {

                    da.DersAdi = ders;
                    db.Ders_Adlari.Add(da);
                    db.SaveChanges();
                    h.Hoca = hoca;
                    h.idHoca = da.id;
                    db.Hocalar1.Add(h);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 6");
                }
                else if (!durum1 && !durum2 && durum3)
                {
                    da.DersAdi = ders;
                    db.Ders_Adlari.Add(da);
                    db.SaveChanges();

                    ds.DersKodu = dersKodu;
                    ds.Kredi = kredi;
                    ds.Saat = saat;
                    ds.idDers = da.id;
                    db.Ders_Secim.Add(ds);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 7");
                }
                else if (durum1 && !durum2 && durum3)
                {
                    string realid = id[0].id.ToString();
                    ds.DersKodu = dersKodu;
                    ds.Kredi = kredi;
                    ds.Saat = saat;
                    ds.idDers = Convert.ToInt32(realid);
                    db.Ders_Secim.Add(ds);
                    db.SaveChanges();
                    MessageBox.Show("Kaydetme işlemi başarılı 8");
                }


                listele();
                txtremove();
            }

        }
        public void txtremove()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            ders_kredisi.Text = "";
            ders_saati.Text = "";
            comboBox3.Text = "";
        }

        private void likeAdmin_Load(object sender, EventArgs e)
        {
            listele();
            fillcombo();
        }
        public void listele()
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                var sorgu = from d1 in db.Ders_Secim
                            join d2 in db.Ders_Adlari on d1.idDers equals d2.id
                            join d3 in db.Hocalar1 on d1.idDers equals d3.idHoca
                            orderby d1.id ascending
                            select new
                            {
                                Ders_Kodu = d1.DersKodu,
                                Kredi = d1.Kredi,
                                Saat = d1.Saat,
                                Ders = d2.DersAdi,
                                Hoca = d3.Hoca
                            };
                //var sorgu = db.son;
                _source.DataSource = sorgu.ToList();
                dataGridView1.DataSource = _source;
            }
        }

        public void fillcombo()
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                List<Ders_Adlari> dersler = db.Ders_Adlari.ToList();

                foreach (var item in dersler)
                {
                    comboBox1.Items.Add(item.DersAdi);
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;

                tablo_ders= dataGridView1.Rows[e.RowIndex].Cells["Ders"].FormattedValue.ToString();
                tablo_hoca = dataGridView1.Rows[e.RowIndex].Cells["Hoca"].FormattedValue.ToString();
                tablo_kod = dataGridView1.Rows[e.RowIndex].Cells["Ders_Kodu"].FormattedValue.ToString();


                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Ders_Kodu"].FormattedValue.ToString();
                ders_kredisi.Text = dataGridView1.Rows[e.RowIndex].Cells["Kredi"].FormattedValue.ToString();
                ders_saati.Text = dataGridView1.Rows[e.RowIndex].Cells["Saat"].FormattedValue.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Ders"].FormattedValue.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Hoca"].FormattedValue.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                string ders = Convert.ToString(comboBox1.Text);
                string dersKodu = Convert.ToString(comboBox2.Text);
                string kredi = Convert.ToString(ders_kredisi.Text);
                string saat = Convert.ToString(ders_saati.Text);
                string hoca = Convert.ToString(comboBox3.Text);


                List<Ders_Adlari> id = db.Ders_Adlari.Where(c => c.DersAdi == tablo_ders).ToList();
                int realid0 = id[0].id;

                List<Ders_Secim> id1 = db.Ders_Secim.Where(a => a.DersKodu == tablo_kod).ToList();
                int realid1 = id1[0].id;

                List<Hocalar1> id2 = db.Hocalar1.Where(b => b.Hoca == tablo_hoca).ToList();
                int realid2 = id2[0].id;

                var z = db.Ders_Adlari.Find(Convert.ToInt32(realid0));
                z.DersAdi = Convert.ToString(comboBox1.Text);
                db.SaveChanges();

                var x = db.Ders_Secim.Find(Convert.ToInt32(realid1));
                x.DersKodu = Convert.ToString(comboBox2.Text);
                x.Kredi = Convert.ToString(ders_kredisi.Text);
                x.Saat = Convert.ToString(ders_saati.Text);
                db.SaveChanges();

                var y = db.Hocalar1.Find(Convert.ToInt32(realid2));
                y.Hoca = Convert.ToString(comboBox3.Text);
                db.SaveChanges();

                listele();
                MessageBox.Show("Güncelleme işlemi başarılı");
                txtremove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                string hoca = comboBox3.Text;
                List<Hocalar1> id = db.Hocalar1.Where(a => a.Hoca == hoca).ToList();
                string realid = id[0].id.ToString();
                var x = db.Hocalar1.Find(Convert.ToInt32(realid));
                db.Hocalar1.Remove(x);
                db.SaveChanges();
                listele();
                MessageBox.Show("Silme işlemi başarılı");
                txtremove();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                string bul = comboBox1.SelectedItem.ToString();
                List<Ders_Adlari> id = db.Ders_Adlari.Where(x => x.DersAdi == bul).ToList();
                int ID = Convert.ToInt32(id[0].id);
                List<Ders_Secim> ders_kodlari = db.Ders_Secim.Where(x => x.idDers == ID).ToList();
                List<Hocalar1> hocalar = db.Hocalar1.Where(x => x.idHoca == ID).ToList();

                foreach (var item in ders_kodlari)
                {
                    comboBox2.Items.Add(item.DersKodu);
                }

                foreach (var item in hocalar)
                {
                    comboBox3.Items.Add(item.Hoca);
                }
            }
        }

        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (KayıtSistemiEntities db = new KayıtSistemiEntities())
            {
                var sorgu = from d1 in db.Ders_Secim
                            join d2 in db.Ders_Adlari on d1.idDers equals d2.id
                            join d3 in db.Hocalar1 on d1.idDers equals d3.idHoca
                            orderby d1.id ascending
                            select new
                            {
                                Ders_Kodu = d1.DersKodu,
                                Kredi = d1.Kredi,
                                Saat = d1.Saat,
                                Ders = d2.DersAdi,
                                Hoca = d3.Hoca
                            };

                string aranan = textBox1.Text;
                var degerler = from item in sorgu
                               where item.Ders.Contains(aranan)
                               select item;
                _source.DataSource = degerler.ToList();
                dataGridView1.DataSource = _source;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
