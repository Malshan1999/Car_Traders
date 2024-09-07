using Car_Traders.Forms;
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

namespace Car_Traders
{
    public partial class CarControl : UserControl
    {
        public CarControl()
        {
            InitializeComponent();
        }
        public string CarName
        {
            get { return LabelCarName.Text; }
            set { LabelCarName.Text = value; }
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

        public Image CarImage
        {
            get { return PictureBoxCarImage.Image; }
            set { PictureBoxCarImage.Image = value; }
        }
        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            // Confirm the purchase
            
        }

        private void BtnBuyNow_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Do you want to buy the {CarName}?", "Confirm Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                            command.Parameters.AddWithValue("@ProductName", CarName);
                            command.Parameters.AddWithValue("@ProductType", "Car ");
                            command.Parameters.AddWithValue("@Price", decimal.Parse(Price));

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"{CarName} has been successfully added to your orders.", "Order Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while processing your order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
