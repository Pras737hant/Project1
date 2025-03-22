using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using WorldPopulationApp;

namespace login_details
{
    public partial class Form4 : Form
    {
        private string connectionString = "server=localhost;user=root;password=;database=world;";
        private string selectCountry;

        public Form4()
        {
            InitializeComponent();
            SetupAutoComplete();
            LoadData();
        }

        private void SetupAutoComplete()
        {
            // Enable AutoComplete for all ComboBoxes
            SetupComboBox(comboBox1);
            SetupComboBox(comboBox2);
            SetupComboBox(comboBox3);
            SetupComboBox(comboBox4);
            SetupComboBox(comboBox5);
            SetupComboBox(comboBox6);
        }

        private void SetupComboBox(ComboBox comboBox)
        {
            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void LoadData()
        {
            LoadComboBox(comboBox1, "SELECT DISTINCT Continent FROM country ORDER BY Continent");  // Continents
            LoadComboBox(comboBox2, "SELECT DISTINCT Region FROM country ORDER BY Region");  // Regions (Fixed Incorrect Query)
            LoadComboBox(comboBox3, "SELECT DISTINCT Name FROM country ORDER BY Name");  // Countries
            LoadComboBox(comboBox4, "SELECT DISTINCT District FROM city ORDER BY District");  // Districts
            LoadComboBox(comboBox5, "SELECT DISTINCT Name FROM city ORDER BY Name");  // Cities
            LoadComboBox(comboBox6, "SELECT DISTINCT Language FROM countrylanguage ORDER BY Language");  // Languages
        }

        private void LoadComboBox(ComboBox comboBox, string query, string parameter = null)
        {
            try
            {
                comboBox.Items.Clear();
                comboBox.AutoCompleteCustomSource.Clear(); // Clear previous autocomplete data

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameter != null)
                        {
                            cmd.Parameters.AddWithValue("@param", parameter);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string item = reader[0].ToString();
                                comboBox.Items.Add(item);
                                comboBox.AutoCompleteCustomSource.Add(item); // Add to AutoComplete
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text)) return;

            string selectedContinent = comboBox1.Text;
            string query = "SELECT DISTINCT Region FROM country WHERE Continent = @param ORDER BY Region";
            LoadComboBox(comboBox2, query, selectedContinent);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox3.Text)) return;

            string selectedCountry = comboBox3.Text;

            string cityQuery = "SELECT DISTINCT Name FROM city WHERE CountryCode = (SELECT Code FROM country WHERE Name = @param LIMIT 1) ORDER BY Name";
            string districtQuery = "SELECT DISTINCT District FROM city WHERE CountryCode = (SELECT Code FROM country WHERE Name = @param LIMIT 1) ORDER BY District";
            string languageQuery = "SELECT DISTINCT Language FROM countrylanguage WHERE CountryCode = (SELECT Code FROM country WHERE Name = @param LIMIT 1) ORDER BY Language";

            LoadComboBox(comboBox5, cityQuery, selectedCountry);
            LoadComboBox(comboBox4, districtQuery, selectedCountry);
            LoadComboBox(comboBox6, languageQuery, selectedCountry);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select a continent.");
                return;
            }

            string selectedContinent = comboBox1.Text;

            // Open Form5 and pass the selected continent
            Form5 form5 = new Form5(selectedContinent);
            form5.Show();
            this.Hide(); // Hide Form4
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Another Action Performed!");
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select a continent.");
                return;
            }

            string selectedContinent = comboBox1.Text;

            // Open Form5 and pass the selected continent
            Form5 form5 = new Form5(selectedContinent);
            form5.Show();
            this.Hide(); // Hide Form4
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Assume comboBox1 contains region names, or set your own way of selecting a region.
            if (comboBox2.SelectedItem != null)
            {
                string selectedRegion = comboBox2.SelectedItem.ToString();
                Form6 form6 = new Form6(selectedRegion);  // Pass selected region to Form6
                form6.Show(); // Show Form6
            }
            else
            {
                MessageBox.Show("Please select a region before proceeding.");
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                string selectedCountry = comboBox3.SelectedItem.ToString(); // Get selected country name
                Form7 form7 = new Form7(selectedCountry);  // Pass country name to Form7
                form7.Show(); // Show Form7
            }
            else
            {
                MessageBox.Show("Please select a country before proceeding.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                string selectedState = comboBox4.SelectedItem.ToString(); // Get selected state
                Form8 form8 = new Form8(selectedState);  // Pass state name to Form8
                form8.Show(); // Show Form8
            }
            else
            {
                MessageBox.Show("Please select a state before proceeding.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem != null)
            {
                string selectedCity = comboBox5.SelectedItem.ToString(); // Get selected state
                Form9 form9 = new Form9(selectedCity);  // Pass state name to Form8
                form9.Show(); // Show Form8
            }
            else
            {
                MessageBox.Show("Please select a city before proceeding.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem != null)
            {
                string selectedLanguage = comboBox6.SelectedItem.ToString(); // Get selected state
                Form9 form9 = new Form9(selectedLanguage);  // Pass state name to Form8
                form9.Show(); // Show Form8
            }
            else
            {
                MessageBox.Show("Please select a city before proceeding.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12();
            form12.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14();
            form14.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form15 form15 = new Form15();
            form15.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.Show();
        }
    }
}