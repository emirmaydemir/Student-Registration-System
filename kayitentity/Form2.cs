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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        KayıtSistemiEntities db = new KayıtSistemiEntities();

        private void Form2_Load(object sender, EventArgs e)
        {
            addcheck();
            listele();
        }
        public void addcheck()
        {
            DataGridViewCheckBoxColumn chx = new DataGridViewCheckBoxColumn();
            chx.HeaderText = "Onay";
            chx.Name = "CheckBox";
            dataGridView1.Columns.Add(chx);
        }
        
        public void listele()
        {
            dataGridView1.DataSource = db.Secilen_Dersler.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Selected)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                    var x = db.Secilen_Dersler.Find(id);
                    db.Secilen_Dersler.Remove(x);
                    db.SaveChanges();
                    MessageBox.Show("Silme işlemi başarılı");
                    listele();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var all = from c in db.Secilen_Dersler select c;
            db.Secilen_Dersler.RemoveRange(all);
            db.SaveChanges();
            listele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string aranan = textBox1.Text;
            var degerler = from item in db.Secilen_Dersler
                           where item.Ders.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
                       
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
