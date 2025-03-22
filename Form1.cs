using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;
using LoginDetails;
using login_details;
using YourNamespace;

namespace LoginDetails

{

    public partial class Form1 : Form

    {

        private string connectionString = "server=localhost;user=root;password=;database=login_details";


        public Form1()

        {

            InitializeComponent();

        }


        private void button1_Click(
            object sender, EventArgs e) // Login button

        {

            string username = textBox1.Text;

            string password = textBox2.Text;


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))

            {

                MessageBox.Show("Please enter Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }


            try

            {

                using (MySqlConnection conn = new MySqlConnection(connectionString))

                {

                    conn.Open();

                    string query = "SELECT * FROM details WHERE username = @username AND password = @password";


                    using (MySqlCommand cmd = new MySqlCommand(query, conn))

                    {

                        cmd.Parameters.AddWithValue("@username", username);

                        cmd.Parameters.AddWithValue("@password", password); // Store passwords securely in real-world apps


                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // If user exists
                            {
                                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Redirect to Form3
                                Form4 form4 = new Form4();
                                form4.Show();
                                this.Hide(); // Hide the current Form1
                            }
                            else
                            {
                                MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }



            catch (Exception ex)

            {

                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


        private void button2_Click(object sender, EventArgs e) // Cancel button

        {

            this.Close();

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) // Show Password
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false; //to show password
            }
            else
            {
                textBox2.UseSystemPasswordChar = true; // hide password
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Forgot Password

        {

            // Redirect to Form3
            Form3 form3= new Form3();
            form3.Show();
            this.Hide(); // Hide the current Form1


        }

        private void linkLabel1_Click_3(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();  // Hide Form1
            form2.ShowDialog(); // Show Form2 as a modal window
            this.Show();  //
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}