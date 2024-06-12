using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Chestionar_Auto
{
    public partial class SignUpWindow : Form
    {
        

        public SignUpWindow()
        {
            InitializeComponent();
        }

        
        public void FolosesteConexiune(MySqlConnection connection)
        {
            
            string query = "SELECT * FROM CONT";
            MySqlCommand command = new MySqlCommand(query, connection);

            
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                    Console.WriteLine(reader.GetString(0));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string username = textBoxUsername.Text;
            string parola = textBoxPassword.Text;
            string email = textBoxEmail.Text;
            string telefon = textBoxTelefon.Text;
            MySqlConnection a = new MySqlConnection();
            BazaDeDate bd = new BazaDeDate();
            a = bd.GetConnection();
            Regex checkEmail = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
            Regex checkPass = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            bool EsteParola = checkPass.IsMatch(parola);
            bool EsteEmail = checkEmail.IsMatch(email);
            if (EsteParola)
            {
                if (EsteEmail)
                {

                    using (MySqlConnection conexiune = bd.GetConnection())
                    {
                        try
                        {
                            string query = "INSERT INTO CONT (username, parola, email, nr_de_telefon) VALUES (@username, @parola, @email, @nr_de_telefon)";

                            using (MySqlCommand comanda = new MySqlCommand(query, conexiune))
                            {

                                comanda.Parameters.AddWithValue("@username", username);
                                comanda.Parameters.AddWithValue("@parola", parola);
                                comanda.Parameters.AddWithValue("@email", email);
                                comanda.Parameters.AddWithValue("@nr_de_telefon", telefon);


                                int randuriAfectate = comanda.ExecuteNonQuery();


                                if (randuriAfectate > 0)
                                {
                                    Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                                    MessageBox.Show("Contul a fost creat cu succes!");
                                    this.Hide();
                                    LogInWindow c = new LogInWindow();
                                    c.Show();
                                }
                                else
                                {
                                    Console.WriteLine("Nu s-au inserat date.");
                                }
                            }
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ati introdus un email neexistent! Va rugam sa incercati din nou!");
                }
            }
            else
            {
                MessageBox.Show("Ati introdus un format gresit al parolei! Va rugam sa aveti minim 8 caractere, cifre si litere mari SI mici.");
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogInWindow l = new LogInWindow();
            l.Show();
            this.Hide();
        }
    }
}

