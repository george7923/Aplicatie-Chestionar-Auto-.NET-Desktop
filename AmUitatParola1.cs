using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;

namespace Chestionar_Auto
{
    public partial class AmUitatParola1 : Form
    {
        private static int COD_RECUPERARE;
        private static int LOL;
        private string email;
        private string EmailTransfer;
        public AmUitatParola1()
        {
            InitializeComponent();
            Random r = new Random();
            COD_RECUPERARE = r.Next(10000);
            LOL = COD_RECUPERARE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            email = this.textBox1.Text;
            EmailTransfer = email;
            string emailGasit = "";
            BazaDeDate bd = new BazaDeDate();
            MySqlConnection CcC = bd.GetConnection();
            string query = "SELECT email FROM cont WHERE email = @email";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, CcC))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emailGasit = reader.GetString(0);
                            

                        }
                    }
                }
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (emailGasit == email)
            {
                
                string subiect = "Resetare parola";
                string continut = "Buna ziua draga client! Pentru a va reseta parola, aveti aici un cod de resetare: Va rog sa-l introduceti atunci cand va resetati parola: " + COD_RECUPERARE.ToString();
                try
                {
                    SendEmail(email, subiect, continut);

                }catch(Exception ex)
                {
                    MessageBox.Show("Ceva nu a mers bine!");
                }
            }
        }
        private void SendEmail(string destinatar, string subiect, string continut)
        {
            string adresaEmail = "kobrageorge792@gmail.com";
            string parolaEmail = "rfkpnureydmdfrvx";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); 
            client.EnableSsl = true; 
            client.UseDefaultCredentials = false; 
            client.Credentials = new NetworkCredential(adresaEmail, parolaEmail); 

            MailMessage email = new MailMessage(adresaEmail, destinatar, subiect, continut);
            try
            {
                client.Send(email);
                MessageBox.Show("Email trimis cu succes!");
                AmUitatParola2 d = new AmUitatParola2(COD_RECUPERARE,EmailTransfer);
                d.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la trimiterea emailului: " + ex.Message);
            }
            finally
            {
                email.Dispose(); 
            }
        }
        public int getCod()
        {
            return LOL;
        }
        public string getEmail()
        {
            return email;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
