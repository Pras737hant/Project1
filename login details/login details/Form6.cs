using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form6 : Form
    {
        private string connectionString = "server=localhost;user=root;password=;database=world;";
        private string selectedRegion;

        public Form6(string region)
        {
            InitializeComponent();
            selectedRegion = region;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch Region Name, Country Name, Country Code, Population, Language
                    string query = @"
                        SELECT c.Region, c.Name AS CountryName, c.Code AS CountryCode, c.Population, cl.Language 
                        FROM country c 
                        LEFT JOIN countrylanguage cl ON c.Code = cl.CountryCode 
                        WHERE c.Region = @Region";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Region", selectedRegion);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt; // Show data in DataGridView
                    }

                    // Query to get the total population of the region
                    string populationQuery = "SELECT SUM(Population) FROM country WHERE Region = @Region";
                    using (MySqlCommand cmd = new MySqlCommand(populationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Region", selectedRegion);
                        object result = cmd.ExecuteScalar();
                        textBox1.Text = result != null ? result.ToString() : "0"; // Show total population in textBox1
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}
