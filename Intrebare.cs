using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chestionar_Auto
{
    public class Intrebare
    {
        public string intrebare { get; set; }
        public Raspuns Raspuns1 { get; set; }
        public Raspuns Raspuns2 { get; set; }
        public Raspuns Raspuns3 { get; set; }
        public string Categorie { get; set; }
        public bool AFostDeja { get; set; }
        public Image img {  get; set; }
        private int ID {  get; set; }

        public Intrebare (string i, Raspuns r1, Raspuns r2, Raspuns r3, string c, bool afd, int id, Image img)
        {
            this.intrebare = i;
            this.Raspuns1 = r1;
            this.Raspuns2 = r2;
            this.Raspuns3 = r3;
            this.Categorie = c;
            this.AFostDeja = afd;
            this.ID = id;
            this.img = img;
        }
        
        
    }
}
