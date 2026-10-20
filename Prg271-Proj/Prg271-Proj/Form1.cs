using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prg271_Proj
{
    public partial class Form1 : Form
    }
        DataHandler handler = new DataHandler();
 BusinessLogic logic = new BusinessLogic();
public Form1()
{
    InitializeComponent();
    btnEditDone.Enabled = false;
    btnCheck.Enabled = false;
    
   
    handler.getAllData();

    BindingSource source = new BindingSource();
    source.DataSource= handler.animals;
    dgvView.DataSource= source;
}


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnCheck.Enabled = true;
 btnAdd.Enabled = false;
 btnView.Enabled = false;
 btnDelete.Enabled = false;


 int indexRow;
 indexRow = dgvView.CurrentRow.Index;
 DataGridViewRow row = dgvView.Rows[indexRow];

 txtID.Text = row.Cells[0].Value.ToString();
 txtName.Text = row.Cells[1].Value.ToString();
 txtSpecies.Text = row.Cells[2].Value.ToString();
 txtAge.Text = row.Cells[3].Value.ToString();
 txtScore.Text = row.Cells[4].Value.ToString();
 txtStatus.Text = "leave Blank";
 txtUnit.Text = "leave Blank";
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
 if (logic.ConditionsMet(txtID.Text, txtName.Text, txtSpecies.Text, txtAge.Text,txtScore.Text))
 {
     btnEditDone.Enabled = true;
     btnCheck.Enabled = false;
     MessageBox.Show("ALL ENTERED DATA IS VALID"); ///add check for blnk in business logic
                                                   ///method to find staus and Stuff then put it in the text boxex
                                                   ///then call edit list method and it takes all the text boxes as input
 }
        }

        private void btnEditDone_Click(object sender, EventArgs e)
        {
 handler.editList(txtID.Text, txtName.Text, txtSpecies.Text, txtAge.Text, txtScore.Text, txtStatus.Text, txtUnit.Text);
 handler.updateFile();
 btnEditDone.Enabled = false;
 MessageBox.Show("Changes were sucessfull");

 btnAdd.Enabled = true;
 btnView.Enabled = true;
 btnDelete.Enabled = true;
        }
    }
}
