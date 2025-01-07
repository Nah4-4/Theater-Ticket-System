using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Theater_Ticket_System
{
    public partial class theaterSelect : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=" + Environment.GetEnvironmentVariable("PASSWORD") + ";database=medalliontheater";
        int patronId;
        public theaterSelect(int patronId)
        {
            InitializeComponent();
            this.patronId = patronId;
            createPerformance();
        }
        void createPerformance()
        {
            panel1.BringToFront();
            string query = "SELECT DISTINCT title FROM performance;";
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                //CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
            };

            panel1.Controls.Add(tableLayoutPanel);

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
                        // Execute the command
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are rows to read
                            if (reader.HasRows)
                            {
                                // Read the data
                                while (reader.Read())
                                {   
                                    var buttons = new Button()
                                    {
                                        Text = reader["title"].ToString(),
                                        Font = new System.Drawing.Font("Gadugi", 12F),
                                        FlatStyle = FlatStyle.Flat,
                                        Size = new Size(300, 70),
                                    };
                                    buttons.Click += new EventHandler(Buttons_Click);                                  
                                    tableLayoutPanel.Controls.Add(buttons);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No Play's Listed.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
            }
        }
        void Buttons_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                panel2.Visible = true;
                panel1.Visible = false;
                string title = clickedButton.Text;
                string query = $"SELECT `performanceid`,`Date`,`Time`,`Type` FROM medalliontheater.performance where title='{title}';";
               
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
                            using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connectionString))
                            {
                                dataAdapter.Fill(dataTable);
                            }
                            dateDGV.DataSource = dataTable;
                            dateDGV.Columns[0].Visible=false;
                            dateDGV.Rows[0].Selected = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
                }

            }
        }       

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (dateDGV.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dateDGV.SelectedCells[0].RowIndex;
                int performanceId = (int)dateDGV.Rows[selectedRowIndex].Cells[0].Value;
                seatReserve seatReserve = new seatReserve(patronId,performanceId);
                seatReserve.ShowDialog();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }
    }

}
