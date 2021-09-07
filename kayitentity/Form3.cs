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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private BindingSource _source = new BindingSource();
        KayıtSistemiEntities db = new KayıtSistemiEntities();
        private void Form3_Load(object sender, EventArgs e)
        {
            listele();
            addcheck();
            fillcombo();
        }
        public void listele()
        {
            //var x = db.Ders_Secim.Where(w => w.id > 2).OrderBy(o => o.Kredi).ToList();
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
            _source.DataSource = sorgu.ToList();
            dataGridView1.DataSource = _source;
        }
        public void addcheck()
        {
            DataGridViewCheckBoxColumn chx = new DataGridViewCheckBoxColumn();
            chx.HeaderText = "Onay";
            chx.Name = "CheckBox";
            dataGridView1.Columns.Add(chx);
        }
        public void fillcombo()
        {
            DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();
            DataGridViewComboBoxColumn combo2 = new DataGridViewComboBoxColumn();
            dataGridView1.Columns.Add(combo2);

            //List<string> liste = new List<string>();
            //DataTable dt = docombo();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //string ders = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    combo = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[6];
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value).Equals(Convert.ToString(dataGridView1.Rows[j].Cells[0].Value)))
                    {
                        combo.Items.Add(Convert.ToString(dataGridView1.Rows[j].Cells[4].Value));
                    }
                } //dataGridView1.ColumnAdded(combo);
            }

            for(int i = 0; i < 5; i++)
            {
               dataremovee();
            }
            //for (int i = 0; i < 3; i++)
           // {
             // dataremovee();
            //}
        }
        public void dataremovee()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = i + 1; j < (dataGridView1.Rows.Count) ; j++)
                {
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value).Equals(Convert.ToString(dataGridView1.Rows[j].Cells[0].Value)))
                    {
                        //MessageBox.Show(Convert.ToString(dataGridView1.Rows[j].Cells[0].Value));
                        dataGridView1.Rows.RemoveAt(j);
                        dataGridView1.Refresh();

                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[5].Selected)
                {
                    string kod = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    string kredi = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                    string saat = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                    string ders = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value);
                    string hoca = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    Secilen_Dersler sc = new Secilen_Dersler();
                    sc.Ders = ders;
                    sc.Kod = kod;
                    sc.Kredi = kredi;
                    sc.Saat = saat;
                    sc.Hoca = hoca;
                    db.Secilen_Dersler.Add(sc);
                    db.SaveChanges();
                    MessageBox.Show("Ders ekleme işlemi başarılı");
                }
            }
            Form2 f2 = new Form2();
            f2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[5].Value = row.Cells[5].Value == null ? false : !(bool)row.Cells[5].Value;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
