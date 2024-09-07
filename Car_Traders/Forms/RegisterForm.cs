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

            // Validate input: Check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (contact.Length != 10 || !contact.All(char.IsDigit))
            {
                MessageBox.Show("The contact number must be exactly 10 digits long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!email.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Email must be a Gmail address (ending with @gmail.com).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 8 ||
        !       System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-zA-Z]") ||  // At least one letter
        !       System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]") ||    // At least one number
        !       System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]"))      // At least one special character
            {
                MessageBox.Show("Password must be at least 8 characters long and include at least one letter, one number, and one symbol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Check if the username already exists
                    SqlCommand checkUsernameCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
                    checkUsernameCommand.Parameters.AddWithValue("@Username", username);

                    int usernameCount = (int)checkUsernameCommand.ExecuteScalar();
                    if (usernameCount > 0)
                    {
                        MessageBox.Show("The username is already taken. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the email already exists
                    SqlCommand checkEmailCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Email = @Email", connection);
                    checkEmailCommand.Parameters.AddWithValue("@Email", email);

                    int emailCount = (int)checkEmailCommand.ExecuteScalar();
                    if (emailCount > 0)
                    {
                        MessageBox.Show("The email is already in use. Please choose a different email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the contact already exists
                    SqlCommand checkContactCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Contact = @Contact", connection);
                    checkContactCommand.Parameters.AddWithValue("@Contact", contact);

                    int contactCount = (int)checkContactCommand.ExecuteScalar();
                    if (contactCount > 0)
                    {
                        MessageBox.Show("The contact number is already in use. Please choose a different contact number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the NIC already exists
                    SqlCommand checkNICCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE NIC = @NIC", connection);
                    checkNICCommand.Parameters.AddWithValue("@NIC", nic);

                    int nicCount = (int)checkNICCommand.ExecuteScalar();
                    if (nicCount > 0)
                    {
                        MessageBox.Show("The NIC is already registered. Please check your details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Insert new user if no duplicates found
                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT INTO Users (Name, Username, Email, Contact, NIC, Address, Role, Password) VALUES (@Name, @Username, @Email, @Contact, @NIC, @Address, 'Customer', @Password)",
                        connection);
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Contact", contact);
                    insertCommand.Parameters.AddWithValue("@NIC", nic);
                    insertCommand.Parameters.AddWithValue("@Address", address);
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);

                    insertCommand.ExecuteNonQuery();

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
                mail.To.Add(email); 
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
