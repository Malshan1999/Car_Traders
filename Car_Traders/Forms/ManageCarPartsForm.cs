using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Traders.Forms
{
    public partial class ManageCarPartsForm : Form
    {
        public ManageCarPartsForm()
        {
            InitializeComponent();
        }

        private void ManageCarPartsForm_Load(object sender, EventArgs e)
        {
            LoadCarParts();
        }
        private void LoadCarParts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM CarParts", connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable carPartsTable = new DataTable();
                            adapter.Fill(carPartsTable);
                            PartGrid.DataSource = carPartsTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading car parts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddPart_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPartId.Text == "" || txtBrand.Text == "" || txtWarrenty.Text == "" || txtPrice.Text == "" || txtQnt.Text == "" || picPart.Image == null)
            {
                MessageBox.Show("All fields, including the image, are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text;
            string carModel = txtPartId.Text;
            string brand = txtBrand.Text;
            string wrnty = txtWarrenty.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int qnt = int.Parse(txtQnt.Text);
            string imageName = txtImageName.Text;
            byte[] imageBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                picPart.Image.Save(ms, picPart.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO CarParts (Name, CarModel, Brand, Warranty, Price, Quantity, ImageName, ImageData) VALUES (@Name, @CarModel, @Brand, @Warranty, @Price, @Quantity, @ImageName, @ImageData)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@CarModel", carModel);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@Warranty", wrnty);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Quantity", qnt);
                        command.Parameters.AddWithValue("@ImageName", imageName);
                        command.Parameters.AddWithValue("@ImageData", imageBytes);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("New Car Part Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form fields
                txtName.Text = "";
                txtPartId.Text = "";
                txtBrand.Text = "";
                txtWarrenty.Text = "";
                txtPrice.Text = "";
                txtQnt.Text = "";
                txtImageName.Text = "";
                picPart.Image = null;

                LoadCarParts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please try again! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                   
                    string selectedFilePath = openFileDialog.FileName;

                    txtImageName.Text = selectedFilePath;

                    picPart.Image = Image.FromFile(selectedFilePath);
                    picPart.SizeMode = PictureBoxSizeMode.StretchImage; // Optional: To make the image fit the PictureBox
                }
            }
        }

        private void PartGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPartId.Text == "" || txtBrand.Text == "" || txtWarrenty.Text == "" || txtPrice.Text == "" || txtQnt.Text == "" || picPart.Image == null)
            {
                MessageBox.Show("All fields, including the image, are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text;
            string carModel = txtPartId.Text;
            string brand = txtBrand.Text;
            string wrnty = txtWarrenty.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int qnt = int.Parse(txtQnt.Text);
            string imageName = txtImageName.Text;
            byte[] imageBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                picPart.Image.Save(ms, picPart.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE CarParts SET Name = @Name, Brand = @Brand, Warranty = @Warranty, Price = @Price, Quantity = @Quantity, ImageName = @ImageName, ImageData = @ImageData WHERE CarModel = @CarModel", connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@CarModel", carModel);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@Warranty", wrnty);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Quantity", qnt);
                        command.Parameters.AddWithValue("@ImageName", imageName);
                        command.Parameters.AddWithValue("@ImageData", imageBytes);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Car Part Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtName.Text = "";
                txtPartId.Text = "";
                txtBrand.Text = "";
                txtWarrenty.Text = "";
                txtPrice.Text = "";
                txtQnt.Text = "";
                txtImageName.Text = "";
                picPart.Image = null;

                LoadCarParts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating car part: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            if (txtPartId.Text == "")
            {
                MessageBox.Show("Please enter the Car Part ID to delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string carModel = txtPartId.Text;

                try
                {
                    using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("DELETE FROM CarParts WHERE CarModel = @CarModel", connection))
                        {
                            command.Parameters.AddWithValue("@CarModel", carModel);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Car Part Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Car Part not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    txtName.Text = "";
                    txtPartId.Text = "";
                    txtBrand.Text = "";
                    txtWarrenty.Text = "";
                    txtPrice.Text = "";
                    txtQnt.Text = "";
                    txtImageName.Text = "";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting car part: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Please enter a search term!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string searchTerm = txtSearch.Text;
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM CarParts WHERE Name LIKE @SearchTerm OR CarModel LIKE @SearchTerm";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        PartGrid.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching for car parts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Optional: If you want to search as the user types
            btnSearch_Click(sender, e);
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all the text fields
            txtName.Text = "";
            txtPartId.Text = "";
            txtBrand.Text = "";
            txtWarrenty.Text = "";
            txtPrice.Text = "";
            txtQnt.Text = "";
            txtImageName.Text = "";

            picPart.Image = null;
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadCarParts();
        }

        private void txtImageName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
