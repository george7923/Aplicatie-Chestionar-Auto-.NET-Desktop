using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chestionar_Auto
{
    public partial class LogInWindow : Form
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            if (username != "" && password != "")
            {
                string userGasit = "";
                string passGasit = "";
                BazaDeDate bd = new BazaDeDate();
                MySqlConnection conn = bd.GetConnection();
                string query = "SELECT Parola FROM cont WHERE Username = @username";

                try
                {

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                passGasit = reader.GetString(0);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Eroare la interogare: " + ex.Message);
                }


                if (password == passGasit)
                {
                    ManagementVariabileGlobale.SetUserName(username);
                    if (username == "Administrator")
                    {
                        FisaAdministrator b = new FisaAdministrator();

                        this.Hide();
                        b.Show();
                    }
                    else
                    {
                        Meniu c = new Meniu();

                        c.Show();
                        this.Hide();
                    }
                }
                else if (password == "")
                {
                    MessageBox.Show("Parola incorectă!");
                }
                else
                {
                    MessageBox.Show("Parola incorectă!");
                }

            }
            else
            {
                MessageBox.Show("Introduceti un username si o parola!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AmUitatParola1 a = new AmUitatParola1();
            a.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignUpWindow s = new SignUpWindow();
            s.Show();
            this.Hide();
        }
    }
}
