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

            // Check the ID before calling ConditionsMet, because that method splits on '-'.
            if (!System.Text.RegularExpressions.Regex.IsMatch(id, @"^WR-[0-9]{4}$"))
            {
                MessageBox.Show("Animal ID must look like WR-0001.");
                txtID.Focus();
                return;
            }

            if (!logic.ConditionsMet(id, name, species, age, score))
            {
                // Shared validation is handled by BusinessLogic.
                return;
            }

            int parsedScore;
            if (!int.TryParse(score, out parsedScore) || parsedScore < 0 || parsedScore > 100)
            {
                MessageBox.Show("Recovery Score must be a whole number between 0 and 100.");
                txtScore.Focus();
                return;
            }

            if (handler.animals.Any(a => string.Equals(a.AnimalID, id, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("An animal with this ID already exists.");
                txtID.Focus();
                return;
            }

            string status;
            string unit;
            DataHandler.ClassifyScore(parsedScore, out status, out unit);

            Animal animal = new Animal(id, name, species, age, score, status, unit);
            try
            {
                // DataHandler adds the animal to its list and writes the record to disk.
                handler.AddAnimal(animal);
                dgvView.DataSource = null;
                dgvView.DataSource = handler.animals;
                txtStatus.Text = status;
                txtUnit.Text = unit;
                MessageBox.Show("Animal added successfully. Status: " + status + ", Housing: " + unit);

                txtID.Clear();
                txtName.Clear();
                txtSpecies.Clear();
                txtAge.Clear();
                txtScore.Clear();
                txtID.Focus();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Permission denied while saving animals.txt.");
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Could not save the animal: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
