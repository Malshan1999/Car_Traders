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
    public partial class AdminCars : Form
    {
        public AdminCars()
        {
            InitializeComponent();
        }

        private void CarGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtManufacturer.Text == "" || txtYear.Text == "" || cmb_FuelType.SelectedItem == null || txtColor.Text == "" || txtPrice.Text == "" || CarPicture.Image == null)
            {
                MessageBox.Show("All fields, including the image, are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text;
            string mnfct = txtManufacturer.Text;
            int year = int.Parse(txtYear.Text);
            string fuel = cmb_FuelType.SelectedItem.ToString();
            string color = txtColor.Text;
            int price = int.Parse(txtPrice.Text);
            string imageName = txtImageName.Text;  // Ensure this is set correctly and contains only the file name
            byte[] imageBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                CarPicture.Image.Save(ms, CarPicture.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Cars (Name, Manufacturer, Year, FuelType, Color, ImageName, ImageData, Price) VALUES (@Name, @Manufacturer, @Year, @FuelType, @Color, @ImageName, @ImageData, @Price)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Manufacturer", mnfct);
                        command.Parameters.AddWithValue("@Year", year);
                        command.Parameters.AddWithValue("@FuelType", fuel);
                        command.Parameters.AddWithValue("@Color", color);
                        command.Parameters.AddWithValue("@ImageName", imageName);  // Store file name
                        command.Parameters.AddWithValue("@ImageData", imageBytes);  // Store image data
                        command.Parameters.AddWithValue("@Price", price);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("New Car Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form fields
                txtName.Text = "";
                txtManufacturer.Text = "";
                txtYear.Text = "";
                cmb_FuelType.SelectedIndex = -1;
                txtColor.Text = "";
                txtPrice.Text = "";
                CarPicture.Image = null;

                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please try again! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        private void txtFuel_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    // Get the file name only, not the full path
                    string selectedFilePath = openFileDialog.FileName;

                    txtImageName.Text = selectedFilePath;

                    CarPicture.Image = Image.FromFile(selectedFilePath);
                    CarPicture.SizeMode = PictureBoxSizeMode.StretchImage; // Optional: To make the image fit the PictureBox
                }
            }
        }




        private void picCar_Click(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadCars()
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Cars", connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row["ImageData"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])row["ImageData"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    Image image = Image.FromStream(ms);
                                    // Do something with the image, like assigning it to a PictureBox
                                }
                            }
                        }

                        CarGrid.DataSource = dataTable;
                        CarGrid.AutoGenerateColumns = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cars: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Search term is empty! Please fill it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadCars(); // Assuming LoadCars() populates the DataGridView with all cars
                return;
            }

            string searchTerm = txtSearch.Text;
            bool isIntString = searchTerm.All(char.IsDigit);

            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    string query = "";

                    if (isIntString)
                    {
                        // Search by Year
                        query = "SELECT * FROM Cars WHERE Year = @Year";
                    }
                    else
                    {
                        // Search by Model or Name
                        query = "SELECT * FROM Cars WHERE Name LIKE @SearchTerm";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (isIntString)
                        {
                            command.Parameters.AddWithValue("@Year", int.Parse(searchTerm));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        CarGrid.DataSource = dataTable;
                        CarGrid.AutoGenerateColumns = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching for cars: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadCars();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void txtCarId_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdminCars_Load(object sender, EventArgs e)
        {
            LoadCars();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {

        }
    }
}
