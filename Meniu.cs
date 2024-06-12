using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chestionar_Auto
{
    public partial class Meniu : Form
    {
        public Meniu()
        {
            InitializeComponent();

            MessageBox.Show(ManagementVariabileGlobale.GetUserName());
            label1.Text = "Bine ati venit "+ ManagementVariabileGlobale.GetUserName() + " la Chestionarul Auto DRPCIV! Cu noi usor obtii 26/26!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HighScore h = new HighScore();
            h.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ModDeJoc = "EXAMEN";
            ManagementVariabileGlobale.SetMod(ModDeJoc);
            PaginaStart p = new PaginaStart();
            p.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ModDeJoc = "INVATARE";
            ManagementVariabileGlobale.SetMod(ModDeJoc);
            PaginaStart p = new PaginaStart();
            p.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Predictii_Performante pf = new Predictii_Performante();
            pf.Show();
        }
    }
}
