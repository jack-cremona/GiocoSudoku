using System;
using System.Collections.Generic;
using System.Text;

namespace GiocoSudoku
{
    internal class Griglia
    {
        public Casella3x3[,] Matrice = new Casella3x3[3, 3];

        int[] valori = new int[] { 5, 3, 0, 6, 0, 0, 0, 9, 8 };
    }
}
