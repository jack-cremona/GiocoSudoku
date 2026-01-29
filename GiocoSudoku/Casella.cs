using System;
using System.Collections.Generic;
using System.Text;

namespace GiocoSudoku
{
    internal class Casella
    {
        public int Valore {  get; set; }
        public bool Modificabile { get; set; }

        public Casella(int valore, bool modificabile)
        {
            Valore = valore;
            Modificabile = modificabile;
        }
    }
}
