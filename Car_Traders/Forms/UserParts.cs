using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Traders.Forms
{
    public partial class UserParts : Form
    {
        public UserParts()
        {
            InitializeComponent();
           
            this.Load += new System.EventHandler(this.UserParts_Load);
        }

        private void UserParts_Load(object sender, EventArgs e)
        {
            LoadCarParts();
        }

        private void LoadCarParts()
        {
            flowLayoutPanelCarParts.Controls.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT Name, Brand, Price, ImageName FROM CarParts", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarPartControl partControl = new CarPartControl
                                {
                                    PartName = reader["Name"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Price = reader["Price"].ToString()
                                };

                                string imagePath = reader["ImageName"].ToString();
                                if (File.Exists(imagePath))
                                {
                                    partControl.PartImage = Image.FromFile(imagePath);
                                }

                                flowLayoutPanelCarParts.Controls.Add(partControl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading car parts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadCarParts(searchText);
        }

        private void LoadCarParts(string name = "", string brand = "")
        {
            flowLayoutPanelCarParts.Controls.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();

                    string query = "SELECT Name, Brand, Price, ImageName FROM CarParts WHERE 1=1";
                    if (!string.IsNullOrEmpty(name))
                    {
                        query += " AND Name LIKE @Name";
                    }
                    if (!string.IsNullOrEmpty(brand))
                    {
                        query += " AND Brand LIKE @Brand";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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
                                CarPartControl partControl = new CarPartControl
                                {
                                    PartName = reader["Name"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Price = reader["Price"].ToString()
                                };

                                string imagePath = reader["ImageName"].ToString();
                                if (File.Exists(imagePath))
                                {
                                    partControl.PartImage = Image.FromFile(imagePath);
                                }

                                flowLayoutPanelCarParts.Controls.Add(partControl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading car parts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
