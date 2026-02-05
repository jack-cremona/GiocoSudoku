using System;
using System.Collections.Generic;
using System.Text;

namespace GiocoSudoku
{
    internal class Casella
    {
        public int Valore {  get; set; }
        public bool Modificabile {  get; set; }

        public int riga { get; set; }   
        public int colonna { get; set; }

        public Casella(int valore, int riga, int colonna)
        {
            Valore = valore;
            if (valore == 0)
            {
                Modificabile = true;
            }
            else
            {
                Modificabile = false;
            }

            this.riga = riga;
            this.colonna = colonna;
        }

        public int GetCasella()
        {
            return Valore;

        }
    }
}
