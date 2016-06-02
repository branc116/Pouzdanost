using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Pouzdanost
{
    
    class Kvadrat
    {
        public struct komponent
        {
            bool rijesen;
            string ime;
            int dubinaId;
            int sirinaId;
            double vrijednost;
            int NumDjece;
            int NumRoditelj;
            Point Loc;
            int[] Djeca;
            int[] Roditelji;
            public komponent(int fuuckingbullshitthatisntworthanythingmydefoutingnotenablienngparamaterlessconstructionfuckyoucsharpfuckyou)
            {
                rijesen = false;
                ime = "Src";
                dubinaId = 0;
                sirinaId = 0;
                vrijednost = 1;
                NumDjece = 0;
                NumRoditelj = 0;
                Loc = new Point(0, 0);
                Djeca = null;
                Roditelji = null;
            }
            void dodaj(string ime, int id, int roditelj) {

            }

        };

        protected komponent[] ogGraf;
        Kvadrat()
        {
            
            ogGraf = new komponent[5];
        }
        

    }

}
