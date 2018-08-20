using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Depolu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView2.FullRowSelect = true;
            listView1.FullRowSelect = true;
        }

        

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\GitHub\arm\Depolu\Depolu\depo.mdb");
        OleDbCommand komut = new OleDbCommand();

        OleDbConnection baglan = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\GitHub\arm\Depolu\Depolu\envanter.mdb");
        OleDbCommand command = new OleDbCommand();

        private void verilerigöster()
        {
            listView1.Items.Clear();
            baglanti.Open();

            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "Select * From Depo ";

            OleDbDataReader reader = komut.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = reader["Adi"].ToString();
                add.SubItems.Add(reader["Marka"].ToString());
                add.SubItems.Add(reader["Model"].ToString());
                add.SubItems.Add(reader["Seri_No"].ToString());
                add.SubItems.Add(reader["Lokasyon"].ToString());
                add.SubItems.Add(reader["Tim"].ToString());
                add.SubItems.Add(reader["Barkod"].ToString());
                add.SubItems.Add(reader["Ariza_Durumu"].ToString());

                listView1.Items.Add(add);
            }
            baglanti.Close();

            listView2.Items.Clear();
            baglan.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = baglan;
            command.CommandText = "Select * From envanter ";

            OleDbDataReader oku = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["Adi"].ToString();
                ekle.SubItems.Add(oku["Marka"].ToString());
                ekle.SubItems.Add(oku["Model"].ToString());
                ekle.SubItems.Add(oku["Seri_No"].ToString());
                ekle.SubItems.Add(oku["Lokasyon"].ToString());
                ekle.SubItems.Add(oku["Tim"].ToString());
                ekle.SubItems.Add(oku["Barkod"].ToString());
                ekle.SubItems.Add(oku["Ariza_Durumu"].ToString());

                listView2.Items.Add(ekle);
            }
            baglan.Close();

        }

        private void listview2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView2.DoDragDrop(listView2.SelectedItems, DragDropEffects.Move);
        }
        private void listview2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void listview2_DragDrop(object sender, DragEventArgs e)
        {
            if(listView2.SelectedItems.Count == 0)
            { return; }
            Point Pt = listView2.PointToClient(new Point(e.X, e.Y));
            ListViewItem ItemDrag = listView2.GetItemAt(Pt.X, Pt.Y);

            if(ItemDrag == null) { return; }

            int ItemDragIndex = ItemDrag.Index;
            ListViewItem[] Sel = new ListViewItem[listView2.SelectedItems.Count];

            for(int i=0; i< listView2.SelectedItems.Count; i++)
            {
                Sel[i] = listView2.SelectedItems[i];
            }
            for(int i=0; i<Sel.GetLength(0); i++)
            {
                ListViewItem Item = Sel[i];
                int ItemIndex = ItemDragIndex;

                if( ItemIndex == Item.Index) { return; }

                if(Item.Index < ItemIndex) { ItemIndex++; }
                else { ItemIndex = ItemDragIndex + 1; }

                ListViewItem InserItem = (ListViewItem)Item.Clone();
                listView2.Items.Insert(ItemIndex, InserItem);
                listView2.Items.Remove(Item);
            }
            listView2.Items.Add(e.Data.GetData(DataFormats.StringFormat).ToString());
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView1.DoDragDrop(listView1.SelectedItems, DragDropEffects.Move);
        }
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            { return; }
            Point Pt = listView1.PointToClient(new Point(e.X, e.Y));
            ListViewItem ItemDrag = listView1.GetItemAt(Pt.X, Pt.Y);

            if (ItemDrag == null) { return; }

            int ItemDragIndex = ItemDrag.Index;
            ListViewItem[] Sel = new ListViewItem[listView1.SelectedItems.Count];

            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                Sel[i] = listView1.SelectedItems[i];
            }
            for (int i = 0; i < Sel.GetLength(0); i++)
            {
                ListViewItem Item = Sel[i];
                int ItemIndex = ItemDragIndex;

                if (ItemIndex == Item.Index) { return; }

                if (Item.Index < ItemIndex) { ItemIndex++; }
                else { ItemIndex = ItemDragIndex + 1; }

                ListViewItem InserItem = (ListViewItem)Item.Clone();
                listView1.Items.Insert(ItemIndex, InserItem);
                listView1.Items.Remove(Item);
            }
            listView1.Items.Add(e.Data.GetData(DataFormats.StringFormat).ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigöster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (listView1.SelectedItems.Count > 0)
            {
                ListViewItem temp = listView1.SelectedItems[0];
                listView1.Items.Remove(temp);
                listView2.Items.Add(temp);
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            while (listView2.SelectedItems.Count > 0)
            {
                ListViewItem temp = listView2.SelectedItems[0];
                listView2.Items.Remove(temp);
                listView1.Items.Add(temp);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT INTO Depo ([Adi],[Marka],[Model],[Seri_No],[Lokasyon],[Tim],[Barkod],[Ariza_Durumu]) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + textBox7.Text.ToString() + "','" + textBox8.Text.ToString() + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigöster();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            MessageBox.Show("Envanter Eklendi", "Başarılı İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;

                cevap = MessageBox.Show("Envanteri silmek istediğinizden emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    
                    baglanti.Open();
                    komut.Connection = baglanti;
                    
                    ListViewItem a = listView1.SelectedItems[0];

                    komut.CommandText = @"DELETE FROM Depo Where [adi] Like '" + a.Text + "'";

                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        item.Remove();
                    }
                    komut.ExecuteNonQuery();
                    
                    baglanti.Close();
                    verilerigöster();
                    MessageBox.Show("Envanter Silindi", "Başarılı İşlem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_DragOver(object sender, DragEventArgs e)
        {
            if(e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        private void form2_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int sayi = listView1.Items.Count;
            int say = listView2.Items.Count;
            MessageBox.Show(sayi + " Depo kaydı ve " +say+ " Saha kaydı bulunmaktadır.", "Sayaç Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}