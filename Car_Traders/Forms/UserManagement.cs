using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Car_Traders.Forms
{
    public partial class UserManagement : Form
    {
        public UserManagement()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var email = txtEmail.Text;
            var nic = txtNIC.Text;
            var address = txtAddress.Text;
            var username = txtUsername.Text;
            var contact = txtContact.Text;
            var password = txtPassword.Text;
            var role = txtRole.Text; // or cmbRole.SelectedItem.ToString();

            // Hash the password
            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    // Insert new user with the hashed password
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Users (Name, Username, Email, Contact, NIC, Address, Role, Password) VALUES (@Name, @Username, @Email, @Contact, @NIC, @Address, @Role, @Password)",
                        connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Contact", contact);
                    command.Parameters.AddWithValue("@NIC", nic);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields after successful registration
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtNIC.Text = "";
                    txtAddress.Text = "";
                    txtUsername.Text = "";
                    txtContact.Text = "";
                    txtPassword.Text = "";
                    txtRole.Text = ""; // or cmbRole.SelectedIndex = -1; if using a ComboBox
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // ComputeHash returns a byte array
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private void UserManagement_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Name, Username, Email, Contact, NIC, Address, Role FROM Users", connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the data to the DataGridView
                    PartNewUser.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
           
            
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtNIC.Text = "";
            txtAddress.Text = "";
            txtUsername.Text = "";
            txtContact.Text = "";
            txtPassword.Text = "";
            txtRole.Text = ""; // or cmbRole.SelectedIndex = -1; if using a ComboBox
        }

        private void PartNewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            
        }

    }
}
