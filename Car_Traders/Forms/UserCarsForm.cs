using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Traders.Forms
{
    public partial class UserCarForm : Form
    {
        public UserCarForm()
        {
            InitializeComponent();
            
            this.Load += new System.EventHandler(this.UserCarForm_Load);
        }

        private void UserCarForm_Load(object sender, EventArgs e)
        {
            LoadCars();
        }

        private void LoadCars()
        {
            flowLayoutPanelCars.Controls.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT Name, Manufacturer AS Brand, Price, ImageName FROM Cars", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarControl carControl = new CarControl
                                {
                                    CarName = reader["Name"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Price = reader["Price"].ToString()
                                };

                                string imagePath = reader["ImageName"].ToString();
                                if (File.Exists(imagePath))
                                {
                                    carControl.CarImage = Image.FromFile(imagePath);
                                }

                                flowLayoutPanelCars.Controls.Add(carControl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cars: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadCars(searchTerm);
        }

        private void UserCarForm_Load_1(object sender, EventArgs e)
        {
            LoadCars();
        }
        private void LoadCars(string name = "", string brand = "")
        {
            flowLayoutPanelCars.Controls.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();

                    // Create query with parameters for search criteria
                    string query = "SELECT Name, Manufacturer AS Brand, Price, ImageName FROM Cars WHERE 1=1";
                    if (!string.IsNullOrEmpty(name))
                    {
                        query += " AND Name LIKE @Name";
                    }
                    if (!string.IsNullOrEmpty(brand))
                    {
                        query += " AND Manufacturer LIKE @Brand";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters if search criteria are provided
                        if (!string.IsNullOrEmpty(name))
                        {
                            command.Parameters.AddWithValue("@Name", "%" + name + "%");
                        }
                        if (!string.IsNullOrEmpty(brand))
                        {
                            command.Parameters.AddWithValue("@Brand", "%" + brand + "%");
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarControl carControl = new CarControl
                                {
                                    CarName = reader["Name"].ToString(),
                                    Brand = reader["Brand"].ToString(),  // Brand is the alias for Manufacturer
                                    Price = reader["Price"].ToString()
                                };

                                string imagePath = reader["ImageName"].ToString();
                                if (File.Exists(imagePath))
                                {
                                    carControl.CarImage = Image.FromFile(imagePath);
                                }

                                flowLayoutPanelCars.Controls.Add(carControl);  // Add control to the panel
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cars: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            
            LoadCars();
        }
    }

}
