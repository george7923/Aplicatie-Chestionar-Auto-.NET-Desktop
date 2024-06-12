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
    public partial class PaginaStart : Form
    {
        public PaginaStart()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chestionarul_PropriuZis a = new Chestionarul_PropriuZis();
            a.Show();
            this.Hide();
        }
    }
}
