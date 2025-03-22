using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form5 : Form
    {
        private string connectionString = "server=localhost;user=root;password=;database=world;";
        private string selectedContinent;

        public Form5(string continent)
        {
            InitializeComponent();
            selectedContinent = continent;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch country details
                    string query = @"
                        SELECT c.Name AS CountryName, c.Code AS CountryCode, c.Population, cl.Language 
                        FROM country c 
                        LEFT JOIN countrylanguage cl ON c.Code = cl.CountryCode 
                        WHERE c.Continent = @Continent";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Continent", selectedContinent);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }

                    // Fetch total population
                    string populationQuery = "SELECT SUM(Population) FROM country WHERE Continent = @Continent";
                    using (MySqlCommand cmd = new MySqlCommand(populationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Continent", selectedContinent);
                        object result = cmd.ExecuteScalar();
                        textBox1.Text = result != null ? result.ToString() : "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
