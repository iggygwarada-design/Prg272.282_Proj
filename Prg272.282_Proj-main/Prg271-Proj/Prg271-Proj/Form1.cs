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

            handler.getAllData();

            RefreshGrid();
            SetupSummaryLabel();
            RefreshSummary();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            handler.SaveSummary(summary);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim().ToUpperInvariant();
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string age = txtAge.Text.Trim();
            string score = txtScore.Text.Trim();

            if (!logic.ConditionsMet(id, name, species, age, score))
                return;

            DataHandler.ClassifyScore(int.Parse(score), out string status, out string unit);

            try
            {
                handler.AddAnimal(new Animal(id, name, species, age, score, status, unit));
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            handler.updateFile();
            RefreshGrid();
            RefreshSummary();

            MessageBox.Show("Animal added successfully.");
            txtID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtScore.Clear();
            txtSpecies.Clear();
            txtStatus.Clear();
            txtUnit.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            ////SEE WHAT YOURE DELETING//
            DataGridViewRow row = dgvView.CurrentRow;

            txtID.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtSpecies.Text = row.Cells[2].Value.ToString();
            txtAge.Text = row.Cells[3].Value.ToString();
            txtScore.Text = row.Cells[4].Value.ToString();
            txtStatus.Text = row.Cells[5].Value.ToString();
            txtUnit.Text = row.Cells[6].Value.ToString();
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

            handler.updateFile();
            RefreshGrid();
            RefreshSummary();

            MessageBox.Show("Animal deleted successfully.");
            txtID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtScore.Clear();
            txtSpecies.Clear();
            txtStatus.Clear();
            txtUnit.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            RefreshSummary();
            lblSummary.Visible = true;
            MessageBox.Show(lblSummary.Text, "Animal Summary");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvView.CurrentRow == null)
            {
                MessageBox.Show("Please select an animal to edit first.");
                return;
            }

            btnCheck.Enabled = true;
            btnAdd.Enabled = false;
            btnView.Enabled = false;
            btnDelete.Enabled = false;

            DataGridViewRow row = dgvView.CurrentRow;

            txtID.Text = row.Cells[0].Value.ToString();
            txtID.ReadOnly = true; // ID is the lookup key, so it can't be changed
            txtName.Text = row.Cells[1].Value.ToString();
            txtSpecies.Text = row.Cells[2].Value.ToString();
            txtAge.Text = row.Cells[3].Value.ToString();
            txtScore.Text = row.Cells[4].Value.ToString();
            txtStatus.Text = "leave Blank";
            txtUnit.Text = "leave Blank";
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (logic.ConditionsMet(txtID.Text.Trim(), txtName.Text.Trim(), txtSpecies.Text.Trim(),
                                    txtAge.Text.Trim(), txtScore.Text.Trim()))
            {
                DataHandler.ClassifyScore(int.Parse(txtScore.Text.Trim()), out string status, out string unit);
                txtStatus.Text = status;
                txtUnit.Text = unit;

                btnEditDone.Enabled = true;
                btnCheck.Enabled = false;
                MessageBox.Show("ALL ENTERED DATA IS VALID");
            }
        }

        private void btnEditDone_Click(object sender, EventArgs e)
        {
            try
            {
                handler.editList(txtID.Text.Trim(), txtName.Text.Trim(), txtSpecies.Text.Trim(),
                                 txtAge.Text.Trim(), txtScore.Text.Trim(), txtStatus.Text, txtUnit.Text);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            handler.updateFile();
            RefreshGrid();
            RefreshSummary();

            txtID.ReadOnly = false;
            btnEditDone.Enabled = false;
            btnAdd.Enabled = true;
            btnView.Enabled = true;
            btnDelete.Enabled = true;
            MessageBox.Show("Changes were successful");
            txtID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtScore.Clear();
            txtSpecies.Clear();
            txtStatus.Clear();
            txtUnit.Clear();
        }
    }
}