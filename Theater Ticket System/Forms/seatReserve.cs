using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Theater_Ticket_System
{
    public partial class seatReserve : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=" + Environment.GetEnvironmentVariable("PASSWORD") + ";database=medalliontheater";
        float price;
        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        int patronId, performanceId;
        string seatId;
        System.Windows.Forms.Button seatButton;
        public seatReserve(int patronId,int performanceId)
        {
            InitializeComponent();
            this.patronId = patronId;
            this.performanceId = performanceId;
            CreateSection("Orchestra",6, 30,'A');
            CreateSection("Mezzanine", 8, 30,'G');
            CreateSection("Balcony", 6, 30,'A');
            CreateSection("Box", 4, 2, 'X');
            CreateSection("Box2", 4, 2, 'X');
        }
        private void CreateSection(string sectionlabel,int rows, int cols, char seatletter)
        {
            List<string> reservedSeats = new List<string>();
            Color seatColor = Color.White;
            if (sectionlabel == "Orchestra")
            {
                 seatColor = ColorTranslator.FromHtml("#deebf7");

            }
            else if (sectionlabel == "Mezzanine")
            {
                 seatColor = ColorTranslator.FromHtml("#fbe5d6");
            }
            else if (sectionlabel == "Balcony")
            {
                 seatColor = ColorTranslator.FromHtml("#ccffcc");
            }
            else if (sectionlabel == "Box")
            {
                 seatColor = ColorTranslator.FromHtml("#ffe699");
            }else if (sectionlabel == "Box2")
            {
                 seatColor = ColorTranslator.FromHtml("#ffe699");
            }
            // Section label
            System.Windows.Forms.Label sectionLabel = new System.Windows.Forms.Label
            {
                Text = sectionlabel,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            if (sectionlabel== "Orchestra")
            {
                orchestraPanel.Size = new Size(cols * 30 + 10, rows * 30 + 40);
                orchestraPanel.Controls.Add(sectionLabel);
            }
            else if(sectionlabel == "Mezzanine")
            {
                MezzaninePanel.Size = new Size(cols * 35 + 10, rows * 30 + 40);
                MezzaninePanel.Controls.Add(sectionLabel);
            }
            else if (sectionlabel == "Balcony")
            {
                balconyPanel.Size = new Size(cols * 35 + 10, rows * 30 + 80);
                balconyPanel.Controls.Add(sectionLabel);
            } 
            else if (sectionlabel == "Box")
            {
               boxPanel.Size = new Size(cols * 35+10, rows * 30+40);
               boxPanel.Controls.Add(sectionLabel);
            }
            else if (sectionlabel == "Box2")
            {
                sectionLabel.Text = "Box";
               boxPanel2.Size = new Size(cols * 35+10, rows * 30+40);
               boxPanel2.Controls.Add(sectionLabel);
            }

            //for reserved seats
            string query = $"select `seat` from medalliontheater.seats where seats.`seatid`IN (SELECT `seatid` FROM medalliontheater.ticket where PerformanceID = {performanceId});";
            try
            {
                // Establish a connection to the database
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Connection successful!");
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are rows to read
                            if (reader.HasRows)
                            {
                                // Read the data
                                while (reader.Read())
                                {
                                    reservedSeats.Add(reader.GetString("seat")); // Collect reserved seat names

                                }
                            }                         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);
            }
            int x = 0;
            // Generate seat buttons
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (row == 3&& sectionlabel == "Balcony"&&(col==0||col==cols-1))
                    {
                        continue;
                    }if ((row == 4||row==5)&& sectionlabel == "Balcony"&&(col<3||col>cols-4))
                    {
                        continue;
                    }
                    seatButton = new System.Windows.Forms.Button
                    {                 
                        FlatStyle = System.Windows.Forms.FlatStyle.Flat,                  
                        Size = new Size(23, 23), // Seat button size
                        Location = new Point(col * 30, 35 + row * 30),
                        Name = $"{sectionlabel}_{row}_{col}",
                        BackColor = seatColor, // Default seat color
                    };
                    
                    toolTip.InitialDelay = 300; // Delay before it appears (in milliseconds)
                    if (sectionlabel == "Balcony")
                    {
                        char l = (char)(seatletter + row);
                        if (row == 4 || row == 5)
                        {
                            toolTip.SetToolTip(seatButton, $"{l}{l}{(col - 2)}");
                        }
                        else if (row==3)
                        {
                            toolTip.SetToolTip(seatButton, $"{l}{l}{(col)}");

                        }
                        else
                            toolTip.SetToolTip(seatButton, $"{l}{l}{(col + 1)}");  
                    }
                    else if (sectionlabel == "Box")
                    {
                        x++;
                        toolTip.SetToolTip(seatButton,$"{seatletter}{x}");
                    }else if (sectionlabel == "Box2")
                    {
                        x++;
                        toolTip.SetToolTip(seatButton,$"{seatletter}{x+8}");
                    }
                    else
                        toolTip.SetToolTip(seatButton, $"{(char)(seatletter + row)}{(col + 1)}");

                    if (reservedSeats.Contains(toolTip.GetToolTip(seatButton)))
                    {
                        seatButton.BackColor = ColorTranslator.FromHtml("#f21818");
                        seatButton.Enabled = false; // Disable reserved seats
                    }
                    seatButton.Click += new EventHandler(SeatButton_Click);

                    switch (sectionlabel)
                    {
                        case "Orchestra":
                            seatButton.Tag = "Orchestra";
                            seatButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#acccea");
                            orchestraPanel.Controls.Add(seatButton);
                            break;
                        case "Mezzanine":
                            seatButton.Tag = "Mezzanine";
                            seatButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#f2a570");
                            MezzaninePanel.Controls.Add(seatButton);
                            break;
                        case "Balcony":
                            seatButton.Tag = "Balcony";
                            seatButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#c1f5cc");
                            balconyPanel.Controls.Add(seatButton);
                            break;
                        case "Box":
                            seatButton.Tag = "Box";
                            seatButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#c1f5cc");
                            boxPanel.Controls.Add(seatButton);
                            break;
                        case "Box2":
                            seatButton.Tag = "Box";
                            seatButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#c1f5cc");
                            boxPanel2.Controls.Add(seatButton);
                            break;
                    }
                }
            }      
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button clickedButton = sender as System.Windows.Forms.Button;

            if (clickedButton != null)
            {
                if (clickedButton.BackColor == ColorTranslator.FromHtml("#fa7d07"))
                {
                    MessageBox.Show("This seat is already reserved.", "Seat Reserved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                clickedButton.BackColor = ColorTranslator.FromHtml("#fa7d07");
                seatId = toolTip.GetToolTip(clickedButton);

                if (clickedButton.Tag.Equals("Orchestra"))
                {
                    price = 65.00f;
                }
                else if (clickedButton.Tag.Equals("Mezzanine"))
                {
                    price = 55.00f;
                }
                else if (clickedButton.Tag.Equals("Balcony"))
                {
                    price = 40.00f;   
                }else if (clickedButton.Tag.Equals("Box"))
                {
                    price = 85.00f;   
                }
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(seatId) || price <= 0.0f)
            {
                MessageBox.Show("Please select a valid seat before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = $"INSERT INTO `medalliontheater`.`ticket` (`PatronID`, `PerformanceID`, `SeatID`, `Price`) VALUES ({patronId}, {performanceId},(SELECT `SeatID` FROM medalliontheater.seats WHERE `Seat` = '{seatId}'), {price});";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        
                        //command.Parameters.AddWithValue("@Phone", phone);            

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"Price for the Seat reserved ${price}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seatId = null;
                        price = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
