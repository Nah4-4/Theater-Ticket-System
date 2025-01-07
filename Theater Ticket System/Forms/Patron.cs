using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Theater_Ticket_System
{
    public partial class Patron : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=" + Environment.GetEnvironmentVariable("PASSWORD") + ";database=medalliontheater";
        public Patron()
        {
            InitializeComponent();
            patronDeatails();
            generateReport();
            generateSeatReport();
        }
        private void patronDeatails()
        {
            string query = "SELECT * FROM medalliontheater.patron;";
            try
            {
                // Establish a connection to the database
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Connection successful!");


                    // Create a command
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        // Execute the command
                        using (MySqlDataAdapter dataAdapter= new MySqlDataAdapter(query,connectionString))
                        {
                            dataAdapter.Fill(dataTable);          
                        }
                        dataGridView1.DataSource = dataTable;
                        
                       
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
            }
        }
        private void generateReport()
        {
            string query = "SELECT p.title AS 'Performance Title',COUNT(t.TicketID) AS 'Tickets Sold'FROM medalliontheater.ticket t JOIN medalliontheater.performance p ON t.PerformanceID = p.PerformanceID GROUP BY p.title;";
            try
            {
                // Establish a connection to the database
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Connection successful!");


                    // Create a command
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        // Execute the command
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connectionString))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                        dataGridView3.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
            }
        }private void generateSeatReport()
        {
            string query = "SELECT s.Seat AS'Seat Name', COUNT(TicketID) AS 'Tickets Sold' FROM medalliontheater.ticket t JOIN medalliontheater.seats s ON t.SeatID = s.SeatID GROUP BY s.Seat;";
            try
            {
                // Establish a connection to the database
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Connection successful!");
                    // Create a command
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        // Execute the command
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connectionString))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                        dataGridView2.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
            }
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string email = textBox3.Text.Trim();
            string phone = textBox4.Text.Trim();
            string address = textBox5.Text.Trim();
            string city = textBox6.Text.Trim();
            string state = textBox7.Text.Trim();
            string zipCode = textBox8.Text.Trim();

            // Validate input
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(zipCode) || string.IsNullOrWhiteSpace(state))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query for inserting data
            string query = @"INSERT INTO medalliontheater.patron 
            (`FirstName`, `LastName`, `Email`, `Phone`, `Address`, `City`,`State`,`ZipCode`) 
            VALUES 
            (@FirstName, @LastName, @Email, @Phone, @Address, @City, @State, @ZipCode)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@City", city);
                        command.Parameters.AddWithValue("@State", state);
                        command.Parameters.AddWithValue("@ZipCode", zipCode);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) inserted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ResetFields()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;

            textBox1.Focus(); 
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            patronDeatails();
        }

        private void selectPatron_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int patronId = (int)dataGridView1.Rows[selectedRowIndex].Cells[0].Value;
                theaterSelect theaterselect = new theaterSelect(patronId);
                theaterselect.ShowDialog();
            }
        }
    }
}
