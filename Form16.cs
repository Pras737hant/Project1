using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form16 : Form
    {
        private string connectionString = "server=localhost;database=world;user=root;password=;";

        public Form16()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to get language-wise population
                    string query = @"
                        SELECT cl.Language, SUM(c.Population * cl.Percentage / 100) AS TotalSpeakers
                        FROM countrylanguage cl
                        JOIN country c ON cl.CountryCode = c.Code
                        GROUP BY cl.Language
                        ORDER BY TotalSpeakers DESC;
                    ";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Query to get total population
                    query = "SELECT SUM(Population) AS WorldPopulation FROM country;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        textBox1.Text = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
