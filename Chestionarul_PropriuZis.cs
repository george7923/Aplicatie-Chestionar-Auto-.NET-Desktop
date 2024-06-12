using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chestionar_Auto
{
    public partial class Chestionarul_PropriuZis : Form
    {
        private List<Button> ButoaneleMele = new List<Button>();
        private List<Intrebare> ListaIntrebari = new List<Intrebare>();
        private MySqlConnection a = new MySqlConnection();
        private List<Intrebare> IntrebariUmplute = new List<Intrebare>();
        private List<Raspuns> RaspunsurileExistente = new List<Raspuns>();
        private Intrebare intrebareaSelectata;
        private int contor = 1, RaspCorecte = 0, RaspGresite = 0;
        private bool IsSelectedA = false;
        private bool IsSelectedB = false;
        private bool IsSelectedC = false;
        private List<bool> Selectate = new List<bool>();
        private bool IsSelectedSUBMIT = false;
        private bool A_RaspunsCorect;
        private bool primaintrebare = true;
        private Timer countdownTimer;
        private int remainingSeconds = 30 * 60;
        private bool CheckTheCorrect = false;
        private List<Intrebare> I26;
        private int state = 0;
        private Intrebare Q;
        public Chestionarul_PropriuZis()
        {
            
            InitializeComponent();
            ButoaneleMele.Add(buttonA);
            ButoaneleMele.Add(buttonB);
            ButoaneleMele.Add(buttonC);
            Selectate.Add(IsSelectedA);
            Selectate.Add(IsSelectedB);
            Selectate.Add(IsSelectedC);

            string query = "SELECT I.ID, I.Intrebare, I.Categoria, R.Raspuns, R.EsteCorect, I.Imagine " +
                           "FROM INTREBARI I " +
                           "JOIN RASPUNSURI R ON I.ID = R.Intrebare_ID " +
                           "ORDER BY I.ID";
            BazaDeDate bd = new BazaDeDate();
            bd.OpenConnection();
            a = bd.GetConnection();
            OrganizareIntrebari(ListaIntrebari, query, ButoaneleMele);
            buttonSTART.Text = "SUBMIT";
            FormareIntrebari(ListaIntrebari);
            Q = getRandomQuestion(I26,false);
            GestorChestionar(Q);
            AfisareIntrebari();
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; 
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();



        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {

            remainingSeconds--;


            TimeSpan time = TimeSpan.FromSeconds(remainingSeconds);
            label5.Text = time.ToString(@"mm\:ss");


            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                MessageBox.Show("Timpul a expirat!");
            }
        }
        private void InsertHighScore(string Calificativ)
        {
            int ID = -1;
            string Username = ManagementVariabileGlobale.GetUserName();
            
            string query = "SELECT ID FROM cont WHERE username = '"+Username+"';";
            using (MySqlCommand command = new MySqlCommand(query, a))
            {


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        ID = reader.GetInt32(0);
                        
                    }
                }
            }
            MessageBox.Show(Username+" "+ID.ToString());
            if (ID != -1)
            {
                string query1 = "INSERT INTO highscore (ID_Cont, Timpul_Ramas, IntrebarileRaspunse, IntrebarileGresite, IntrebarileCorecte, Calificativ) VALUES (@IdCont, @TimpRamas, @IntrRaspunse, @Gresite, @Corecte, @Calificativ)";
                using (MySqlCommand comanda = new MySqlCommand(query1, a))
                {
                    
                    //@IdCont, @TimpRamas, @IntrRaspunse, @Gresite, @Corecte, @Calificativ
                    comanda.Parameters.AddWithValue("@IdCont", ID);
                    comanda.Parameters.AddWithValue("@TimpRamas", label5.Text);
                    comanda.Parameters.AddWithValue("@IntrRaspunse", contor);
                    comanda.Parameters.AddWithValue("@Gresite", RaspGresite);
                    comanda.Parameters.AddWithValue("@Corecte", RaspCorecte);
                    comanda.Parameters.AddWithValue("@Calificativ", Calificativ);





                    int randuriAfectate = comanda.ExecuteNonQuery();


                    if (randuriAfectate > 0)
                    {
                        Console.WriteLine($"Datele au fost inserate cu succes. Numărul de rânduri afectate: {randuriAfectate}");


                    }
                    else
                    {
                        Console.WriteLine("Nu s-au inserat date.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ceva nu a mers bine!");
            }
        }
        public void AfisareIntrebari()
        {
            for(int  i = 0; i < ListaIntrebari.Count; i++)
            {
                Console.WriteLine("INTREBAREA NR: " + i.ToString() + ": " + ListaIntrebari[i].intrebare);
                Console.WriteLine(ListaIntrebari[i].Raspuns1.Text + " " + ListaIntrebari[i].Raspuns1.Corect.ToString());
                Console.WriteLine(ListaIntrebari[i].Raspuns2.Text + " " + ListaIntrebari[i].Raspuns2.Corect.ToString());
                Console.WriteLine(ListaIntrebari[i].Raspuns3.Text + " " + ListaIntrebari[i].Raspuns3.Corect.ToString());
            }
        }
        private void arataRaspunsuriCorecte(Intrebare Q)
        {
            if (Q.Raspuns1.Corect)
            {
                buttonA.BackColor = Color.Green;
            }
            else
            {
                buttonA.BackColor = Color.Red;
            }
            if (Q.Raspuns2.Corect)
            {
                buttonB.BackColor = Color.Green;
            }
            else
            {
                buttonB.BackColor = Color.Red;
            }
            if (Q.Raspuns3.Corect)
            {
                buttonC.BackColor = Color.Green;
            }
            else
            {
                buttonC.BackColor = Color.Red;
            }
        }
        private Intrebare getRandomQuestion(List<Intrebare> I, bool isSkip)
        {
            if (I.Count == 0)
            {
                Raspuns R1 = new Raspuns("Raspuns1", false);
                Raspuns R2 = new Raspuns("Raspuns1", false);
                Raspuns R3 = new Raspuns("Raspuns1", false);
                return new Intrebare("Intrebare default", R1, R2, R3, "Random",false, -1, null);

            }
            Random r = new Random();
            int nr = r.Next(0, I.Count-1);
            Intrebare INTR = I[nr];
            if (!isSkip)
            {
                I.Remove(I[nr]);

            }
            return INTR;
        }
        private void FormareIntrebari(List<Intrebare> I)
        {
            I26 = new List<Intrebare>();
            if (I.Count == 0)
            {
                throw new Exception("Gol");
            }
            else
            {
                for(int i = 0; i < 26; i++)
                {
                    I26.Add(getRandomQuestion(ListaIntrebari,false));
                }
            }
        }
        private void GestorChestionar( Intrebare Q)
        {
            
                label6.Text = contor.ToString();
                LabelRaspGresite.Text = RaspGresite.ToString();
                LabelRaspCorecte.Text = RaspCorecte.ToString();

                //MessageBox.Show(intr.intrebare );
                
                RaspunsurileExistente.Add(Q.Raspuns1);
                RaspunsurileExistente.Add(Q.Raspuns2);
                RaspunsurileExistente.Add(Q.Raspuns3);
                label_INTREBARE.Text = Q.intrebare;
                label_R1.Text = Q.Raspuns1.Text;
                label_R2.Text = Q.Raspuns2.Text;
                label_R3.Text = Q.Raspuns3.Text;
                pictureBox1.Image = Q.img;
                buttonA.BackColor = Color.White;
            buttonB.BackColor = Color.White;
            buttonC.BackColor = Color.White;
           
                
               
            
            
          
        }
        private void button4_Click(object sender, EventArgs e)
        {
            bool IsSelectedSUBMIT = true;
            string ModDeJoc = ManagementVariabileGlobale.GetMod();
            if (ModDeJoc == "EXAMEN")
            {
                if (primaintrebare)
                {
                    primaintrebare = false;
                    Punctajul(CheckCorectitudine(Q));
                    Q = getRandomQuestion(I26, false);
                    GestorChestionar(Q);
                    // DeselecteazaButoanele();
                    IsSelectedSUBMIT = false;
                }
                else
                {
                    while (IsSelectedSUBMIT)
                    {
                        Punctajul(CheckCorectitudine(Q));

                        if (RaspGresite <= 4)
                        {
                            Q = getRandomQuestion(I26, false);
                            GestorChestionar(Q);
                            // DeselecteazaButoanele();
                            IsSelectedSUBMIT = false;
                        }
                        if (RaspGresite > 4 || label5.Text == "00:00")
                        {
                            InsertHighScore("RESPINS");
                            PICAT p = new PICAT();
                            p.Show();
                            this.Hide();
                            break;
                        }
                        if (RaspCorecte >= 22 && contor > 26)
                        {
                            InsertHighScore("ADMIS");
                            ADMIS p = new ADMIS();
                            p.Show();
                            this.Hide();
                            break;
                        }
                        else
                        {
                            // Nimic nu se intampla
                        }
                    }
                }
            }
            else
            {
                switch (state)
                {
                    case 0:
                        if (primaintrebare)
                        {
                            primaintrebare = false;
                            Punctajul(CheckCorectitudine(Q));
                            state = 1;
                            // Deselecteaza butoanele
                            IsSelectedSUBMIT = false;
                        }
                        else
                        {
                            Punctajul(CheckCorectitudine(Q));
                            if (RaspGresite <= 4 && label5.Text != "00:00" && (RaspCorecte < 22 || contor <= 26))
                            {
                                DeselecteazaButoanele();
                                IsSelectedSUBMIT = false;
                            }
                            else
                            {
                                string calificativ = (RaspGresite > 4 || label5.Text == "00:00") ? "RESPINS" : "ADMIS";
                                if (calificativ == "RESPINS")
                                {
                                    PICAT p = new PICAT();
                                    p.Show();
                                    this.Hide();
                                    break;
                                }
                                else
                                {
                                    ADMIS aDMIS = new ADMIS();
                                    aDMIS.Show();
                                    this.Hide();
                                    break;
                                }
                                
                            }
                        }
                        arataRaspunsuriCorecte(Q);
                        state = 1;
                        break;

                    case 1:
                        
                        Q = getRandomQuestion(I26, false);
                        GestorChestionar(Q);
                        state = 0;
                        break;
                }
            }

        }
        private void AfiseazaRaspunsurileCorecte(List<Raspuns> R)
        {
            List<Button> Buttons = new List<Button>() { buttonA, buttonB, buttonC };


            for (int i = 0; i < R.Count; i++)
            {
                if (R[i].Corect)
                {
                    Buttons[i].BackColor = Color.Green;
                }
                else
                {
                    Buttons[i].BackColor = Color.Red;
                }
            }
            
        }

        private void DeselecteazaButoanele()
        {


            IsSelectedA = false;
            IsSelectedB = false;
            IsSelectedC = false;
            buttonA.BackColor = Color.White;
            buttonB.BackColor = Color.White;
            buttonC.BackColor = Color.White;
        }
        private void Punctajul(bool A_RaspunsCorect)
        {
            contor++;
            if (!A_RaspunsCorect)
            {
                RaspGresite++;
            }
            else
            {
                RaspCorecte++;
                A_RaspunsCorect = false;
            }
        }
        private bool CheckCorectitudine(Intrebare I)
        {

            List<bool> Selected = new List<bool>() { IsSelectedA, IsSelectedB, IsSelectedC };
            bool[,] matrice = new bool[10,10];
            List<bool> R = new List<bool>() { I.Raspuns1.Corect,I.Raspuns2.Corect,I.Raspuns3.Corect };



            /*for(int i = 0; i < R.Count; i++)
            {
                if ((Selected[i] != R[i].Corect)||((Selected[i] == R[i].Corect && (R[i].Corect == false  a fost = false)))) 
                {
                    return false;
                }
            }
            return true;*/
            matrice[0, 0] = Selected[0];
            matrice[0,1] = Selected[1];
            matrice[0,2] = Selected[2];
            matrice[1, 0] = R[0];
            matrice[1, 1] = R[1];
            matrice[1, 2] = R[2];
            for(int i = 0; i < 3; i++)
            {
                if (!matrice[0, i].Equals(matrice[1, i]))
                {
                    DeselecteazaButoanele();
                    return false;
                }
            }
            DeselecteazaButoanele();
            return true;
        }
        private void SeteazaBackgroundButon()
        {
            
        }
        private void buttonA_Click_1(object sender, EventArgs e)
        {
            IsSelectedA = !IsSelectedA;
            if (IsSelectedA)
            {
                buttonA.BackColor = Color.DarkOrange;
            }
            else
            {
                buttonA.BackColor = Color.White;
            }
        }

        private void buttonB_Click_1(object sender, EventArgs e)
        {
            IsSelectedB = !IsSelectedB;
            if (IsSelectedB)
            {
                buttonB.BackColor = Color.DarkOrange;
            }
            else
            {
                buttonB.BackColor = Color.White;
            }
        }

        private void buttonC_Click_1(object sender, EventArgs e)
        {
            IsSelectedC = !IsSelectedC;
            if (IsSelectedC)
            {
                buttonC.BackColor = Color.DarkOrange;
            }
            else
            {
                buttonC.BackColor = Color.White;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Q = getRandomQuestion(I26, true);
            GestorChestionar(Q);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void OrganizareIntrebari(List<Intrebare> ListaIntrebari, string query, List<Button> B)
        {
            using (MySqlCommand command = new MySqlCommand(query, a))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    MessageBox.Show(B.Count.ToString());
                    List<Raspuns> R = new List<Raspuns>();
                    while (reader.Read())
                    {
                        
                        string ID_String = reader.GetInt32("ID").ToString();
                        int ID = Convert.ToInt32(ID_String);
                        string intrebare = reader.GetString("Intrebare"); 
                        string raspuns = reader.GetString("Raspuns"); 
                        bool esteCorect = reader.GetBoolean("EsteCorect"); 
                        string categorie = reader.GetString("Categoria"); 

                        Console.WriteLine("NU E IN LISTA: " + intrebare);
                        

                        byte[] imageBytes = null;
                        Image imagine = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("Imagine")))
                        {
                            imageBytes = (byte[])reader["Imagine"];
                        }

                        if (imageBytes != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                imagine = Image.FromStream(ms);
                            }
                        }

                        if (i % 2 == 0&&i!=0)
                        {
                            Raspuns R_R = new Raspuns(raspuns, esteCorect,B[i]);
                            R.Add(R_R);
                            Intrebare a = new Intrebare(intrebare, R[0], R[1], R[2], categorie, false, ID, imagine);
                            ListaIntrebari.Add(a);
                            for(int j = 0; j < R.Count; j++)
                            {
                                
                                Console.WriteLine(R.Count);
                            }
                            R.Clear();
                            i = 0;
                            
                        }
                        else
                        {
                            Raspuns R_R = new Raspuns(raspuns, esteCorect, B[i]);
                            R.Add(R_R);
                            Console.WriteLine(raspuns);
                            i++;

                        }



                    }
                }
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
