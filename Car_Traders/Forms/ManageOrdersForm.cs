using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Traders.Forms
{
    public partial class ManageOrdersForm : Form
    {
        public ManageOrdersForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(ManageOrdersForm_Load);
        }

        private void ManageOrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Orders";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        OrdersDataGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private Order GetOrderDetails(int orderId)
        {
            Order order = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT OrderID, UserID, Username, Email, ProductName, ProductType, Price, OrderDate FROM Orders WHERE OrderID = @OrderID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                order = new Order
                                {
                                    OrderID = Convert.ToInt32(reader["OrderID"]),
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    ProductType = reader["ProductType"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching order details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return order;
        }

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            if (OrdersDataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    int orderId = Convert.ToInt32(OrdersDataGridView.SelectedRows[0].Cells["OrderID"].Value);

                    // Update order status to 'Confirmed'
                    UpdateOrderStatus(orderId, "Confirmed");


                    Order order = GetOrderDetails(orderId);

                    // Generate PDF report
                    string pdfPath = GenerateOrderReport(order);


                    SendEmailWithAttachment(order.Email, order.Username, pdfPath);

                    MessageBox.Show("Order has been confirmed, report generated, and email sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while confirming the order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to confirm.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SendEmailWithAttachment(string email, string username, string pdfPath)
        {
            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("primecartraderssrilanka@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Your Order Confirmation - ABC Car Traders";
                mail.Body = $"Dear {username},\n\nYour order has been confirmed. Please find the order confirmation report attached.\n\nBest regards,\nABC Car Traders Team";


                Attachment attachment = new Attachment(pdfPath);
                mail.Attachments.Add(attachment);


                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("primecartraderssrilanka@gmail.com", "mhnh snhk zvuo lanf");
                smtpClient.EnableSsl = true;

                // Send the email
                smtpClient.Send(mail);

                MessageBox.Show("Email sent successfully with the report attached.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateOrderReport(Order order)
        {
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Order_{order.OrderID}.pdf");

            try
            {
                using (var document = new iTextSharp.text.Document())
                {
                    PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                    document.Open();

                    // Load the logo image
                    string logoPath = @"C:\Users\USER\Desktop\PrimeCarLogo.jpg";
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(100f, 100f); // Adjust the size as needed
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    document.Add(logo);

                    // Add the title below the logo
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Prime Car Traders")
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD)
                    };
                    document.Add(title);

                    // Add a space before the order details
                    document.Add(new iTextSharp.text.Paragraph(" "));

                    // Order Confirmation Details
                    document.Add(new iTextSharp.text.Paragraph("Order Confirmation Report"));
                    document.Add(new iTextSharp.text.Paragraph($"Order ID: {order.OrderID}"));
                    document.Add(new iTextSharp.text.Paragraph($"Username: {order.Username}"));
                    document.Add(new iTextSharp.text.Paragraph($"Email: {order.Email}"));
                    document.Add(new iTextSharp.text.Paragraph($"Order Date: {order.OrderDate}"));

                    document.Add(new iTextSharp.text.Paragraph(" "));

                    // Create a table for the order items
                    PdfPTable table = new PdfPTable(3);
                    table.WidthPercentage = 100;

                    table.AddCell("Product Name");
                    table.AddCell("Product Type");
                    table.AddCell("Price");

                    table.AddCell(order.ProductName);
                    table.AddCell(order.ProductType);
                    table.AddCell(order.Price.ToString("C"));

                    document.Add(table);

                    // Add a space before the thank you message
                    document.Add(new iTextSharp.text.Paragraph(" "));

                    // Add the "Thank You" message
                    iTextSharp.text.Paragraph thankYouMessage = new iTextSharp.text.Paragraph("Thank you for choosing Prime Car Traders. We appreciate your business and look forward to serving you again!")
                    {
                        Alignment = iTextSharp.text.Element.ALIGN_CENTER,
                        Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.ITALIC)
                    };
                    document.Add(thankYouMessage);

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return pdfPath;
        }




        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (OrdersDataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    int orderId = Convert.ToInt32(OrdersDataGridView.SelectedRows[0].Cells["OrderID"].Value);
                    UpdateOrderStatus(orderId, "Canceled");
                    MessageBox.Show("Order has been canceled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while canceling the order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to cancel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateOrderStatus(int orderId, string status)
        {
            using (SqlConnection connection = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Orders SET OrderStatus = @Status WHERE OrderID = @OrderID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@OrderID", orderId);
                        command.ExecuteNonQuery();
                        LoadOrders();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating the order status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void OrdersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageOrdersForm_Load_1(object sender, EventArgs e)
        {

        }
    }

}