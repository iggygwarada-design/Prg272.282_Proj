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
    {
        DataHandler handler = new DataHandler();
 BusinessLogic logic = new BusinessLogic();
 Label lblSummary = new Label();
public Form1()
{
    InitializeComponent();
    btnEditDone.Enabled = false;
    btnCheck.Enabled = false;
    
   
    try
    {
        handler.getAllData();
    }
    catch (System.IO.IOException ex)
    {
        MessageBox.Show("Unable to load animals.txt: " + ex.Message);
    }
    catch (UnauthorizedAccessException)
    {
        MessageBox.Show("Permission denied while reading animals.txt.");
    }

    BindingSource source = new BindingSource();
    source.DataSource= handler.animals;
    dgvView.DataSource= source;
}


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim().ToUpperInvariant();
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string age = txtAge.Text.Trim();
            string score = txtScore.Text.Trim();

            

            Animal animal = new Animal(id, name, species, age, score, status, unit);
            handeler.AddAnimal(animal);
            handler.updateFile();
        
        
            
        
            }
            private void SetupSummaryLabel()
{
    lblSummary.AutoSize = true;
    lblSummary.Left = dgvView.Left;
    lblSummary.Top = dgvView.Bottom + 10;
    Controls.Add(lblSummary);
}

private void RefreshGrid()
{
    dgvView.DataSource = null;
    dgvView.DataSource = handler.animals;
}

private void RefreshSummary()
{
    string summary = logic.BuildSummary(handler.animals);
    lblSummary.Text = summary;
    TryFileAction(() => handler.SaveSummary(summary));
}

private bool TryFileAction(Action action)
{
    try
    {
        action();
        return true;
    }
    catch (System.IO.IOException ex)
    {
        MessageBox.Show("File error: " + ex.Message);
    }
    catch (UnauthorizedAccessException)
    {
        MessageBox.Show("Permission denied while accessing a file.");
    }
    catch (Exception ex)
    {
        MessageBox.Show("Unexpected error: " + ex.Message);
    }
    return false;
}
    
        }


        private void btnDelete_Click(object sender, EventArgs e)
{
    if (dgvView.CurrentRow == null)
    {
        MessageBox.Show("Please select an animal to delete first.");
        return;
    }

    string id = dgvView.CurrentRow.Cells[0].Value.ToString();
    string name = dgvView.CurrentRow.Cells[1].Value.ToString();

    DialogResult answer = MessageBox.Show(
        "Delete " + name + " (" + id + ")?\nThis cannot be undone.",
        "Confirm Delete",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button2
    );

    if (answer != DialogResult.Yes)
    {
        return;
    }

    if (!handler.DeleteAnimal(id))
    {
        MessageBox.Show("Animal not found.");
        return;
    }

    bool saved = TryFileAction(() => handler.updateFile());

    RefreshGrid();
    RefreshSummary();

    if (saved)
    {
        MessageBox.Show("Animal deleted successfully.");
    }
}

        private void btnView_Click(object sender, EventArgs e)
    {
        RefreshSummary();
        lblSummary.Visible = true;
        MessageBox.Show(lblSummary.Text, "Animal Summary");
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

