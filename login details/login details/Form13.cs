using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_details
{
    public partial class Form13 : Form
    {
        private string connectionString = "server=localhost;database=world;user=root;password=;";

        public Form13()
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

                    // Query to get country-wise population
                    string query = "SELECT Name AS Country, Population FROM country ORDER BY Population DESC;";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Query to get total world population
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
