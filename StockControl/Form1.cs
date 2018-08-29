using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Add("Název");
            listView1.Columns.Add("Hodnota");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FormEdit edit = new FormEdit();
            if (edit.ShowDialog() == DialogResult.Cancel)
                return;

            checkedListBox1.Items.Clear();
            StockControlItem.stockControlList.Keys.ToList().ForEach(x => checkedListBox1.Items.Add(StockControlItem.stockControlList[x].Name));
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            foreach (var item in StockControlItem.stockControlList)
            {

            }
            
        }
      
        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StockControlItem item = StockControlItem.stockControlList[checkedListBox1.SelectedItem.ToString()];

            listView1.Items.Clear();
            listView1.Items.Add(new ListViewItem(new[] { "Název: ",item.Name}));
            listView1.Items.Add(new ListViewItem(new[] { "Ceník: ", item.PriceList }));
            listView1.Items.Add(new ListViewItem(new[] { "Feed: ", item.Feed }));
            listView1.Items.Add(new ListViewItem(new[] { "Je odkaz: ", item.IsUrl.ToString() }));
            listView1.Items.Add(new ListViewItem(new[] { "Název sloupce: ", item.CatalogNumber}));
            listView1.Columns[0].Width = -1;
            listView1.Columns[1].Width = -1;

        }

        
    }

    
}
