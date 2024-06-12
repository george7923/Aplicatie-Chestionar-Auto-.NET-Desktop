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
    public partial class HighScore : Form
    {
        public HighScore()
        {
            InitializeComponent();
            BazaDeDate bd = new BazaDeDate();
            bd.OpenConnection();
            MySqlConnection a;
            a = bd.GetConnection();
            string username = ManagementVariabileGlobale.GetUserName();
            string query = "SELECT\r\n    c.Username AS UTILIZATOR,\r\n    h.Timpul_Ramas,\r\n    h.IntrebarileRaspunse AS INTREBARILE_PARCURSE,\r\n    h.IntrebarileGresite AS GRESELI,\r\n    h.IntrebarileCorecte AS CORECTE,\r\n    h.Calificativ\r\nFROM\r\n    highscore h\r\nJOIN\r\n    cont c ON h.ID_Cont = c.ID\r\nWHERE\r\n    c.Username = '"+username+"';";

            MySqlCommand command = new MySqlCommand(query, a);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private void AfisareHighScore()
        {
            string query = "SELECT\r\n    c.Username AS UTILIZATOR,\r\n    h.Timpul_Ramas,\r\n    h.IntrebarileRaspunse AS INTREBARILE_PARCURSE,\r\n    h.IntrebarileGresite AS GRESELI,\r\n    h.IntrebarileCorecte AS CORECTE,\r\n    h.Calificativ\r\nFROM\r\n    highscore h\r\nJOIN\r\n    cont c ON h.ID_Cont = c.ID\r\nWHERE\r\n    c.Username = '@username';";

        }
        private void HighScore_Load(object sender, EventArgs e)
        {

        }
    }
}
