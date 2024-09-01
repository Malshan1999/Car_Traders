using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car_Traders.Forms;
using System.Security.Cryptography;


using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Car_Traders.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtContact.Text = "";
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtUsername.Focus();
        }


        private void Register_Click(object sender, EventArgs e)
        {
            // Get input values
            var name = txtName.Text;
            var email = txtEmail.Text;
            var nic = txtNIC.Text;
            var address = txtAddress.Text;
            var username = txtUsername.Text;
            var contact = txtContact.Text;
            var password = txtPassword.Text;
            var confirmPassword = txtComPassword.Text;

            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hash the password
            string hashedPassword = HashPassword(password);

            // Insert the new user into the database
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    // Insert new user
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Users (Name, Username, Email, Contact, NIC, Address, Role, Password) VALUES (@Name, @Username, @Email, @Contact, @NIC, @Address, 'Customer', @Password)",
                        connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Contact", contact);
                    command.Parameters.AddWithValue("@NIC", nic);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Send thank you email
                    SendThankYouEmail(email, name);
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


        private void SendThankYouEmail(string email, string name)
        {
            try
            {
                // Create the mail message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("primecartraderssrilanka@gmail.com");
                mail.To.Add(email); // Ensure this email is valid and populated
                mail.Subject = "Welcome to Prime Car Traders!";
                mail.Body = $@"
Dear {name},

Welcome to Prime Car Traders!

Thank you for registering with us. We are thrilled to have you on board. As a valued member of our community, you now have access to our wide range of premium vehicles, vehicle parts and excellent customer service.



If you have any questions or need assistance, feel free to reach out to us at any time. We are here to help you make the most of your experience with us.

Thank you once again for choosing Prime Car Traders. We look forward to serving you!

Best regards,
The Prime Car Traders Team

------------------------------
Prime Car Traders
N0 4, Elpitiya Road,Ambalangoda
Phone: 077 619 8012
Email: support@primecartraders.com

";


                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("primecartraderssrilanka@gmail.com", "mhnh snhk zvuo lanf");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                MessageBox.Show("Confirmation email sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send confirmation email. Error: " + ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void btn_close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void BackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtComPassword.PasswordChar = '•';
            }
        }
    }
}
