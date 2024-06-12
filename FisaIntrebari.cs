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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chestionar_Auto
{
    public partial class FisaIntrebari : Form
    {
        private MySqlConnection a;
        private string ImageLocation;
        public FisaIntrebari()
        {
            BazaDeDate bd = new BazaDeDate();
            bd.OpenConnection();
            a = bd.GetConnection();
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FisaIntrebari_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ID = textBoxAdd_ID.Text;
            string intrebarea = textBoxAdd_Intr.Text;
            string categorie = textBoxAdd_Categorie.Text;
            string raspuns1 = textBoxAdd_Rasp1.Text;
            string raspuns2 = textBoxAdd_Rasp2.Text;
            string raspuns3 = textBoxAdd_Rasp3.Text;
            bool bool1 = checkBox1.Checked;
            bool bool2 = checkBox2.Checked;
            bool bool3 = checkBox3.Checked;
           
            AdaugaIntrebare(intrebarea, categorie, ImageLocation, ID);
            AdaugaRaspuns(raspuns1,raspuns2,raspuns3,bool1,bool2,bool3,ID);
            textBoxAdd_ID.Text = "";
            textBoxAdd_Intr.Text = "";
            textBoxAdd_Rasp1.Text = "";
            textBoxAdd_Rasp2.Text = "";
            textBoxAdd_Rasp3.Text = "";



        }
        private void AdaugaIntrebare(string text, string categoria, string caleImagine, string ID)
        {
            
            string query = "INSERT INTO intrebari (ID, Intrebare, Categoria, Imagine) VALUES (@id, @intrebarea, @categoria, LOAD_FILE(@imaginea))";
            try
            {
                using(MySqlCommand comanda = new MySqlCommand(query, a))
                {
                    comanda.Parameters.AddWithValue("@id", ID);
                    comanda.Parameters.AddWithValue("@intrebarea", text);
                    comanda.Parameters.AddWithValue("@categoria", categoria);
                    comanda.Parameters.AddWithValue("@imaginea", caleImagine);
                    int randuriAfectate = comanda.ExecuteNonQuery();
                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Intrebarea a fost adaugata cu succes!");



                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }

                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AdaugaRaspuns(string Raspuns1, string Raspuns2, string Raspuns3, bool bool1, bool bool2, bool bool3, string ID)
        {
            string queryR = "INSERT INTO raspunsuri (Intrebare_ID, Raspuns, EsteCorect) VALUES (@intrebare_id, @raspuns1, @bool1), (@intrebare_id, @raspuns2, @bool2), (@intrebare_id, @raspuns3, @bool3)";
            try
            {
                using (MySqlCommand comanda = new MySqlCommand(queryR, a))
                {
                    comanda.Parameters.AddWithValue("@intrebare_id", ID);
                    comanda.Parameters.AddWithValue("@raspuns1", Raspuns1);
                    comanda.Parameters.AddWithValue("@raspuns2", Raspuns2);
                    comanda.Parameters.AddWithValue("@raspuns3", Raspuns3);
                    comanda.Parameters.AddWithValue("@bool1", bool1);
                    comanda.Parameters.AddWithValue("@bool2", bool2);
                    comanda.Parameters.AddWithValue("@bool3", bool3);
                    int randuriAfectate = comanda.ExecuteNonQuery();
                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");
                        MessageBox.Show("Răspunsurile au fost adăugate cu succes!");
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


        private void button4_Click(object sender, EventArgs e)
        {
            string ID_INTR = textBoxCautaRasp.Text;
            string query2 = "SELECT * FROM raspunsuri WHERE Intrebare_Id = "+ID_INTR;
            MySqlCommand command2 = new MySqlCommand(query2, a);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable = new DataTable();
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            Grid_Intrebari.DataSource = dataTable;
            Grid_Raspunsuri.DataSource = dataTable2;
            button1.Text = "REFRESH";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ID = textBoxModif_ID.Text;
            string intrebare = textBoxModif_Intr.Text;
            string r1 = textBoxModif_Rasp1.Text;
            string r2 = textBoxModif_Rasp2.Text;
            string r3 = textBoxModif_Rasp3.Text;
            string categoria = textBoxModif_Categ.Text;
            string queryI = "UPDATE intrebari SET Intrebare = @intrebare WHERE ID = @id";
            string queryR = "UPDATE "; // la final

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string ID = textBoxSterg_ID.Text;
            string query = "DELETE FROM intrebari WHERE ID = @id";
            using(MySqlCommand b =  new MySqlCommand(query, a))
            {
                b.Parameters.AddWithValue("@id", ID);
                int randuriAfectate = b.ExecuteNonQuery();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM intrebari";
            string query2 = "SELECT * FROM raspunsuri";
            MySqlCommand command = new MySqlCommand(query, a);
            MySqlCommand command2 = new MySqlCommand(query2, a);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable = new DataTable();
            DataTable dataTable2 = new DataTable();
            adapter.Fill(dataTable);
            adapter2.Fill(dataTable2);
            Grid_Intrebari.DataSource = dataTable;
            Grid_Raspunsuri.DataSource = dataTable2;
            button1.Text = "REFRESH";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            ImageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg|PNG files(*.png)|*.png| All Files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImageLocation = dialog.FileName;
                    pictureBox1.ImageLocation = ImageLocation;

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
