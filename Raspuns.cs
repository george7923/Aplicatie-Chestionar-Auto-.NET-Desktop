using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chestionar_Auto
{
    public class Raspuns
    {
        public string Text { get; set; }
        public bool Corect {  get; set; }
        public Button Button { get; set; }
        public Raspuns(string Text, bool Corect, Button B) 
        { 
            this.Text = Text;
            this.Corect = Corect;
            this.Button = B;
        
        }
        public Raspuns(string Text, bool Corect)
        {
            this.Text = Text;
            this.Corect = Corect;
        }

    }
}
