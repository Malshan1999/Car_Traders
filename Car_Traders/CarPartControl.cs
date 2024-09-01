using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car_Traders.Forms;

namespace Car_Traders
{
    public partial class CarPartControl : UserControl
    {
        public CarPartControl()
        {
            InitializeComponent();
           
        }
        public string PartName
        {
            get { return LabelPartName.Text; }
            set { LabelPartName.Text = value; }
        }

        public string Brand
        {
            get { return LabelBrand.Text; }
            set { LabelBrand.Text = value; }
        }

        public string Price
        {
            get { return LabelPrice.Text; }
            set { LabelPrice.Text = value; }
        }

        public Image PartImage
        {
            get { return PictureBoxPartImage.Image; }
            set { PictureBoxPartImage.Image = value; }
        }

        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            {
                // Implement the logic to handle the Buy Now button click
                DialogResult dialogResult = MessageBox.Show($"Do you want to buy the {PartName}?", "Confirm Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                        {
                            connection.Open();
                            string query = "INSERT INTO Orders (UserID, Username, Email, ProductName, ProductType, Price, OrderStatus) " +
                                           "VALUES (@UserID, @Username, @Email, @ProductName, @ProductType, @Price, 'Pending')";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserID", Global.UserID);
                                command.Parameters.AddWithValue("@Username", Global.Username);
                                command.Parameters.AddWithValue("@Email", Global.Email);
                                command.Parameters.AddWithValue("@ProductName", PartName);
                                command.Parameters.AddWithValue("@ProductType", "Car Part");  // Assuming this is a car part
                                command.Parameters.AddWithValue("@Price", decimal.Parse(Price));

                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show($"{PartName} has been successfully added to your orders.", "Order Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Redirect to the Order Status Form
                        OrderStatusForm orderStatusForm = new OrderStatusForm();
                        orderStatusForm.Show();
                        ((Form)this.TopLevelControl).Hide();  // Hide the current form
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while processing your order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
