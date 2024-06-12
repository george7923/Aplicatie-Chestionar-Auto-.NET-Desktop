using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Chestionar_Auto
{
    public partial class FisaUseri : Form
    {
        private MySqlConnection a;
        private string parola;
        private string email;
        private string username;
        private string telefon;
        public FisaUseri()
        {
            BazaDeDate bd = new BazaDeDate();
            bd.OpenConnection();
            a = bd.GetConnection();
            InitializeComponent();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM cont";
            MySqlCommand command = new MySqlCommand(query, a);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            button1.Text = "REFRESH";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            username = textBox1.Text;
            parola = textBox2.Text;
            email = textBox3.Text;
            telefon = textBox4.Text;
            Regex checkEmail = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
            Regex checkPass = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            bool EsteParola = checkPass.IsMatch(parola);
            bool EsteEmail = checkEmail.IsMatch(email);
            if (username != "")
            {
                if (EsteParola)
                {
                    if (EsteEmail)
                    {
                        
                            try
                            {
                                string query = "INSERT INTO CONT (username, parola, email, nr_de_telefon) VALUES (@username, @parola, @email, @nr_de_telefon)";

                                using (MySqlCommand comanda = new MySqlCommand(query, a))
                                {

                                    comanda.Parameters.AddWithValue("@usernameCautat", username);
                                    comanda.Parameters.AddWithValue("@parola", parola);
                                    comanda.Parameters.AddWithValue("@email", email);
                                    comanda.Parameters.AddWithValue("@nr_de_telefon", telefon);


                                    int randuriAfectate = comanda.ExecuteNonQuery();


                                    if (randuriAfectate > 0)
                                    {
                                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                                        MessageBox.Show("Contul a fost creat cu succes!");
                                      
                                        
                                        
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
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string query = "SELECT * FROM cont WHERE username = '" + textBox5.Text + "'";
            MySqlCommand command = new MySqlCommand(query, a);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            button1.Text = "AFISATI BAZA DE DATE";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string UsernameCautat = textBox6.Text;
            string UpdatedPassword = textBox7.Text;
            string UpdatedEmail = textBox8.Text;
            string UpdatedPhone = textBox9.Text;
            Regex checkEmail = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
            Regex checkPass = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            bool EsteParola = checkPass.IsMatch(UpdatedPassword);
            bool EsteEmail = checkEmail.IsMatch(UpdatedEmail);
            bool RelatieParola = (UpdatedPassword != "" && EsteParola);
            bool RelatieEmail = (UpdatedEmail != "" && EsteEmail);
            bool RelatieTelefon = (UpdatedPhone != "");
            if (RelatieParola&&!RelatieEmail&&!RelatieTelefon)
            {
                ModificaParola(UpdatedPassword, UsernameCautat);
            }
            else if (RelatieParola && RelatieEmail && !RelatieTelefon)
            {
                ModificaParola_Email(UpdatedPassword,UpdatedEmail, UsernameCautat);
            }
            else if(RelatieParola && RelatieEmail && RelatieTelefon)
            {
                ModificaParola_Email_Telefon(UpdatedPassword, UpdatedEmail, UpdatedPhone, UsernameCautat);
            }
            else if(!RelatieParola && RelatieEmail && !RelatieTelefon)
            {
                ModificaEmail(UpdatedEmail, UsernameCautat);
            }
            else if(!RelatieParola && RelatieEmail && RelatieTelefon)
            {
                ModificaEmail_Telefon(UpdatedEmail,UpdatedPhone, UsernameCautat);
            }
            else if (RelatieParola && !RelatieEmail && RelatieTelefon)
            {
                ModificaParola_Telefon(UpdatedPassword,UpdatedPhone, UsernameCautat);
            }
            else
            {
                ModificaTelefon(UpdatedPhone, UsernameCautat);
            }
        }
        //UPDATE cont SET email = @newEmail, nr_de_telefon = @newTelefon WHERE username = @usernameCautat;
        private void ModificaTelefon(string NoulTelefon, string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET nr_de_telefon = @nr_de_telefon WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@nr_de_telefon", NoulTelefon);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numarul de randuri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Ceva nu a mers bine!" + ex.Message);

            }
        }
        private void ModificaParola_Telefon(string NouaParola, string NoulTelefon, string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET parola = @parola, nr_de_telefon = @nr_de_telefon WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@parola", NouaParola);
                    comanda.Parameters.AddWithValue("@nr_de_telefon", NoulTelefon);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Ceva nu a mers bine!" + ex.Message);

            }
        }
        private void ModificaEmail_Telefon(string NoulEmail, string NoulTelefon,  string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET email = @email, nr_de_telefon = @nr_de_telefon WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@email", NoulEmail);
                    comanda.Parameters.AddWithValue("@nr_de_telefon", NoulTelefon);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Ceva nu a mers bine!" + ex.Message);

            }
        }
        private void ModificaEmail(string NoulEmail, string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET email = @email WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@email", NoulEmail);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Ceva nu a mers bine!" + ex.Message);

            }
        }
        private void ModificaParola_Email_Telefon(string NouaParola, string NoulEmail, string NoulTelefon, string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET parola = @parola, email = @email, nr_de_telefon = @nr_de_telefon WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@email", NoulEmail);
                    comanda.Parameters.AddWithValue("@parola", NouaParola);
                    comanda.Parameters.AddWithValue("@nr_de_telefon", NoulTelefon);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Ceva nu a mers bine!" + ex.Message);

            }
        }
        private void ModificaParola_Email(string NouaParola, string NoulEmail, string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET parola = @parola, email = @email WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@email", NoulEmail);
                    comanda.Parameters.AddWithValue("@parola", NouaParola);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Ceva nu a mers bine!" + ex.Message);

            }
        }
        private void ModificaParola(string NouaParola,string UsernameCautat)
        {
            try
            {
                string query = "UPDATE cont SET parola = @parola WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", UsernameCautat);
                    comanda.Parameters.AddWithValue("@parola", NouaParola);
                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Contul a fost creat cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }



            }
            catch(MySqlException ex) 
            { 
            
                MessageBox.Show("Ceva nu a mers bine!" + ex.Message );
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string username = textBox10.Text;
            try
            {
                string query = "DELETE FROM cont WHERE username = @usernameCautat";
                using (MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@usernameCautat", username);
                    int randuriAfectate = comanda.ExecuteNonQuery();

                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost șterse cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Utilizatorul a fost șters cu succes!");
                    }
                    else
                    {
                        Console.WriteLine("Nu s-au șters date.");
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Meniu meniu = new Meniu();
            meniu.Show();
            this.Hide();
        }
    }
}
