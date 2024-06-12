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
    public partial class FisaAdministrator : Form
    {
        public FisaAdministrator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FisaUseri fu = new FisaUseri();
            fu.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Meniu meniu = new Meniu();
            meniu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FisaIntrebari fs = new FisaIntrebari();
            fs.Show();
            this.Hide();
        }
    }
}
