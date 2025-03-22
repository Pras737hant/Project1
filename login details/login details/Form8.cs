using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form8 : Form
    {
        private readonly string connectionString = "server=localhost;user=root;password=;database=world;";
        private readonly string selectedDistrict;

        public Form8(string district)
        {
            InitializeComponent();
            selectedDistrict = district;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch District Name, Population, and Language
                    string query = @"
                        SELECT c.District, c.Population, 
                               IFNULL(cl.Language, 'Unknown') AS Language
                        FROM city c
                        LEFT JOIN countrylanguage cl ON c.CountryCode = cl.CountryCode
                        WHERE c.District = @District";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@District", selectedDistrict);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt; // Show data in DataGridView
                        }
                    }

                    // Query to get the total population of the selected district
                    string populationQuery = "SELECT IFNULL(SUM(Population), 0) FROM city WHERE District = @District";
                    using (MySqlCommand cmd = new MySqlCommand(populationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@District", selectedDistrict);
                        object result = cmd.ExecuteScalar();
                        textBox1.Text = result?.ToString() ?? "0"; // Show total population in TextBox1
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
