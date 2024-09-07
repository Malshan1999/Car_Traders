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
            string imageName = txtImageName.Text;  
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
                        command.Parameters.AddWithValue("@ImageName", imageName);  
                        command.Parameters.AddWithValue("@ImageData", imageBytes);
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
            // Clear all the  fields
            txtName.Text = "";
            txtManufacturer.Text = "";
            txtYear.Text = "";
            txtColor.Text = "";
            txtPrice.Text = "";
            txtImageName.Text = "";  

            
            cmb_FuelType.SelectedIndex = -1;  

            
            CarPicture.Image = null;  

            
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
                LoadCars(); 
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
                        // Convert the search term to an integer (year)
                        int year = int.Parse(searchTerm);

                        // Define the valid range for the year
                        int minYear = 2000;
                        int maxYear = DateTime.Now.Year;

                        // Validate if the year is within the range
                        if (year < minYear || year > maxYear)
                        {
                            MessageBox.Show($"Please enter a valid year between {minYear} and {maxYear}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Search by Year
                        query = "SELECT * FROM Cars WHERE Year = @Year";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Year", year);
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            CarGrid.DataSource = dataTable;
                            CarGrid.AutoGenerateColumns = false;
                        }
                    }
                    else
                    {
                        // Search by  Name
                        query = "SELECT * FROM Cars WHERE Name LIKE @SearchTerm";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            CarGrid.DataSource = dataTable;
                            CarGrid.AutoGenerateColumns = false;
                        }
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
            if (CarGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the selected car's CarID (assuming CarID is a hidden column in the DataGridView)
            int selectedRowIndex = CarGrid.SelectedRows[0].Index;
            int carID = Convert.ToInt32(CarGrid.Rows[selectedRowIndex].Cells["CarID"].Value); // Replace "CarID" with actual column name

            // Validate that all required fields are filled
            if (txtName.Text == "" || txtManufacturer.Text == "" || txtYear.Text == "" || cmb_FuelType.SelectedItem == null || txtColor.Text == "" || txtPrice.Text == "" || CarPicture.Image == null)
            {
                MessageBox.Show("All fields, including the image, are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gather updated information from the form fields
            string name = txtName.Text;
            string manufacturer = txtManufacturer.Text;
            int year = int.Parse(txtYear.Text);
            string fuelType = cmb_FuelType.SelectedItem.ToString();
            string color = txtColor.Text;
            int price = int.Parse(txtPrice.Text);
            string imageName = txtImageName.Text;
            byte[] imageBytes;

            // Convert image to byte array for database storage
            using (MemoryStream ms = new MemoryStream())
            {
                CarPicture.Image.Save(ms, CarPicture.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            // Update the car details in the database
            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();

                    string updateQuery = @"UPDATE Cars
                                   SET Name = @Name, Manufacturer = @Manufacturer, Year = @Year, FuelType = @FuelType, 
                                       Color = @Color, ImageName = @ImageName, ImageData = @ImageData, Price = @Price
                                   WHERE CarID = @CarID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Manufacturer", manufacturer);
                        command.Parameters.AddWithValue("@Year", year);
                        command.Parameters.AddWithValue("@FuelType", fuelType);
                        command.Parameters.AddWithValue("@Color", color);
                        command.Parameters.AddWithValue("@ImageName", imageName);  // Store file name
                        command.Parameters.AddWithValue("@ImageData", imageBytes);  // Store image data
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@CarID", carID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Car details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCars();  // Refresh the list to show updated data
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the car details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating the car: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (CarGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a car to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Assuming the CarID is stored in a hidden column in the DataGridView
            int selectedRowIndex = CarGrid.SelectedRows[0].Index;
            int carID = Convert.ToInt32(CarGrid.Rows[selectedRowIndex].Cells["CarID"].Value); // Replace "CarID" with the actual column name

            // Confirm Deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete this car?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("DELETE FROM Cars WHERE CarID = @CarID", connection))
                        {
                            command.Parameters.AddWithValue("@CarID", carID);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Car deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCars(); // Refresh the car list in the DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the car. It may no longer exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting car: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
