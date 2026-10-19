using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG271._281_PROJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnEditDone.Enabled = false;
            btnCheck.Enabled = false;
            BusinessLogic logic = new BusinessLogic();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
            
               
                btnCheck.Enabled = true; 
                int indexRow;
                indexRow = dgvView.CurrentRow.Index;
                DataGridViewRow row = dgvView.Rows[indexRow];

                txtID.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtSpecies.Text = row.Cells[2].Value.ToString();
                txtAge.Text = row.Cells[3].Value.ToString();
                txtScore.Text = "Leave Blank";
                txtStatus.Text = "leave Blank";
                txtUnit.Text = "leave Blank";
            
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            ////waiting.....
            Thread.Sleep(1000);
            BusinessLogic logic = new BusinessLogic();
           if( logic.ConditionsMet(txtID.Text,txtName.Text,txtSpecies.Text,txtAge.Text))
            { btnEditDone.Enabled = true;
                MessageBox.Show("ALL ENTERED DATA IS VALID");
            }
        }
    }
}
