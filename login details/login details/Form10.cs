using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form10 : Form
    {
        private readonly string connectionString = "server=localhost;user=root;password=;database=world;";
        private readonly string selectedLanguage;

        public Form10(string language)
        {
            InitializeComponent();
            selectedLanguage = language;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch Country Name, City Population, City Name, and Language
                    string query = @"
                SELECT co.Name AS CountryName, 
                       (co.Population * cl.Percentage / 100) AS LanguagePopulation,
                       ci.Name AS City, ci.Population AS CityPopulation, cl.Language 
                FROM countrylanguage cl
                JOIN country co ON cl.CountryCode = co.Code
                JOIN city ci ON co.Code = ci.CountryCode
                WHERE cl.Language = @Language";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Language", selectedLanguage);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt; // Show data in DataGridView
                        }
                    }

                    // Query to calculate the total population using the selected language
                    string totalPopulationQuery = @"
                SELECT SUM(co.Population * cl.Percentage / 100) 
                FROM countrylanguage cl
                JOIN country co ON cl.CountryCode = co.Code
                WHERE cl.Language = @Language";

                    using (MySqlCommand cmd = new MySqlCommand(totalPopulationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Language", selectedLanguage);
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
