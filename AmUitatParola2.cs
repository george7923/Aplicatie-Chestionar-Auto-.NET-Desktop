using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chestionar_Auto
{
    public partial class AmUitatParola2 : Form
    {
        private int secunde = 59;
        private int minute = 2;
        private AmUitatParola1 a = new AmUitatParola1();
        string email;
        int cod;
        bool isRunning = true;
        private System.Windows.Forms.Timer cronometru;
        public AmUitatParola2(int cod_recuperare, String e)
        {
            InitializeComponent();
            
            if (secunde == 0 && minute == 0)
            {
                button1.Enabled = false;
            }
            this.email = e;
            this.cod = cod_recuperare;

        }
        private void StartTimp()
        {
            Thread timpThread = new Thread(GestiuneTimp);
            timpThread.Start();
        }

        private void GestiuneTimp()
        {
            while (minute >= 0 && secunde >= 0 && isRunning)
            {
                string min = minute.ToString("00"); 
                string sec = secunde.ToString("00"); 

                label2.Text = min + ":" + sec;

                if (secunde == 0 && minute > 0)
                {
                    minute--;
                    secunde = 59;
                }
                else
                {
                    secunde--;
                }

                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == cod.ToString())
            {
                button2.Enabled = true;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Cod de recuperare incorect! "+ this.cod.ToString());
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string parola = textBox2.Text;
            
            if (textBox2.Text == textBox3.Text)
            {
                string query = "UPDATE cont SET parola = @parola WHERE email = @email";
                BazaDeDate bd = new BazaDeDate();
                MySqlConnection a = bd.GetConnection();
                using(MySqlCommand cmd = new MySqlCommand(query, a))
                {
                    
                    try
                    {
                        cmd.Parameters.AddWithValue("@parola", parola);
                        cmd.Parameters.AddWithValue("@email", email);
                        int numarRanduriActualizate = cmd.ExecuteNonQuery();

                        
                        if (numarRanduriActualizate > 0)
                        {
                            MessageBox.Show("Parola a fost actualizată cu succes!");
                            Form1 f = new Form1();
                            f.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Nu s-a putut actualiza parola.");
                        }
                    }
                    catch(MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void AmUitatParola2_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false; 
        }
    }
}
