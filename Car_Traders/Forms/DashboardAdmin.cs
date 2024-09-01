using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Traders.Forms
{
    public partial class DashboardAdmin : Form
    {
        public DashboardAdmin()
        {
            InitializeComponent();
        }
        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            loadform(new UserManagement());
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            loadform(new AdminCars());
        }

        private void btnCarParts_Click(object sender, EventArgs e)
        {
            loadform(new ManageCarPartsForm());
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            loadform(new ManageOrdersForm());
        }

        private void btnConfirmOrders_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                this.Close(); // Close the current form
                LoginForm loginForm = new LoginForm(); // Create a new instance of the login form
                loginForm.Show(); // Show the login form
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
