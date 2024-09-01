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

namespace Car_Traders.Forms
{
    public partial class ConfirmedOrdersForm : Form
    {
        public ConfirmedOrdersForm()
        {
            InitializeComponent();
        }
        private void ConfirmedOrdersForm_Load(object sender, EventArgs e)
        {
            LoadConfirmedOrders();
        }

        private void LoadConfirmedOrders()
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Orders WHERE Status = 'Confirmed'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        ConfirmedOrdersDataGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading confirmed orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfirmedOrdersForm_Load_1(object sender, EventArgs e)
        {

        }
    }

}