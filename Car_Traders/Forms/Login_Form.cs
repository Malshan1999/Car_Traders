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

namespace Car_Traders
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            // Checking fields are empty
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {
                MessageBox.Show("Username and Password fields cannot be empty.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }

            string username = txtUsername.Text;
            string password = txtpassword.Text;

            // Hash the entered password
            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    // Check username and hashed password with DB
                    string query = "SELECT UserId, Name, Username, Email, Role FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword); // Use the hashed password

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();

                            MessageBox.Show("You are logged in successfully.", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            Global.UserID = reader["UserId"].ToString();
                            Global.Username = reader["Username"].ToString();
                            Global.Name = reader["Name"].ToString();
                            Global.Email = reader["Email"].ToString();
                            Global.UserRole = reader["Role"].ToString();

                            // Check user role and navigate 
                            if (Global.UserRole == "Admin")
                            {
                                new Forms.DashboardAdmin().Show();
                            }
                            else if (Global.UserRole == "Customer")
                            {
                                new Forms.DashboardUser().Show();
                            }

                            // Clear text fields
                            txtUsername.Text = "";
                            txtpassword.Text = "";
                            this.Hide(); // Hide the login form
                        }
                        else
                        {
                            MessageBox.Show("Username or Password is incorrect.", "Incorrect Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtpassword.Text = "";
            txtUsername.Focus();
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtpassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '•';
            }
        }

        private void ToRegisterForm_Click(object sender, EventArgs e)
        {
            new Forms.RegisterForm().Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            //this.Close();
            System.Windows.Forms.Application.Exit();
        }
    }
}
