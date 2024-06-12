using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Chestionar_Auto
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogInWindow hey = new LogInWindow();
            hey.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUpWindow signup = new SignUpWindow();
            signup.Show();
            this.Hide();
        }
    }
}
