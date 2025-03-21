using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form7 : Form
    {
        private readonly string connectionString = "server=localhost;user=root;password=;database=world;";
        private readonly string selectedCountry;

        public Form7(string country)
        {
            InitializeComponent();
            selectedCountry = country;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch Country Code, Population, and Language
                    string query = @"
                        SELECT c.Code AS CountryCode, c.Population, 
                               IFNULL(cl.Language, 'Unknown') AS Language
                        FROM country c 
                        LEFT JOIN countrylanguage cl ON c.Code = cl.CountryCode 
                        WHERE c.Name = @Country";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Country", selectedCountry);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt; // Show data in DataGridView
                        }
                    }

                    // Query to get the total population of the selected country
                    string populationQuery = "SELECT IFNULL(Population, 0) FROM country WHERE Name = @Country";
                    using (MySqlCommand cmd = new MySqlCommand(populationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Country", selectedCountry);
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
