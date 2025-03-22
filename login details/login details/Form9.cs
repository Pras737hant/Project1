using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form9 : Form
    {
        private readonly string connectionString = "server=localhost;user=root;password=;database=world;";
        private readonly string selectedCity;

        public Form9(string cityName)
        {
            InitializeComponent();
            selectedCity = cityName;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch City, Population, CountryCode, District, and Language
                    string query = @"
                        SELECT c.Name AS City, c.CityPopulation, c.CountryCode, c.District, 
                               IFNULL(cl.Language, 'Unknown') AS Language
                        FROM city c
                        LEFT JOIN countrylanguage cl ON c.CountryCode = cl.CountryCode
                        WHERE c.Name = @City";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@City", selectedCity);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt; // Show data in DataGridView
                        }
                    }

                    // Query to get the total population of the selected city
                    string populationQuery = "SELECT IFNULL(Population, 0) FROM city WHERE Name = @City";
                    using (MySqlCommand cmd = new MySqlCommand(populationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@City", selectedCity);
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
