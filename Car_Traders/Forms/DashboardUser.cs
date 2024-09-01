using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car_Traders.Forms;


namespace Car_Traders.Forms
{
    public partial class DashboardUser : Form
    {
        
        public DashboardUser()
        {
            InitializeComponent();
            
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            loadform(new UserCarForm());
        }

        private void btnCarParts_Click(object sender, EventArgs e)
        {
            loadform(new UserParts());
        }
        public void loadform(Form form)
        {
            if (form == null)
            {
                throw new ArgumentException("Invalid form passed to loadform method");
            }

            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(form);
            this.mainpanel.Tag = form;
            form.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            loadform(new OrderStatusForm());
        }

        private void btnConfirmOrders_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                this.Close(); // Close the current form
                LoginForm loginForm = new LoginForm(); // Create a new instance of the login form
                loginForm.Show(); // Show the login form
            }

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
           
        }
        private string loggedInUsername;

        public DashboardUser(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }
    }
    
}
