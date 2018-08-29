using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockControl
{
    public partial class FormEdit : Form
    {
        public FormEdit()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtBoxFeed.Text=="" || txtBoxId.Text == "" || txtBoxPrice.Text == "" || txtBoxName.Text == "" )
            {
                MessageBox.Show("Doplň chybějící hodnoty!","Chybějící hodnoty",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            StockControlItem item = new StockControlItem(txtBoxName.Text,txtBoxPrice.Text,txtBoxId.Text,txtBoxFeed.Text,chckBoxFeed.Checked);
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
