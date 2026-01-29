using System;
using System.Collections.Generic;
using System.Text;

namespace GiocoSudoku
{
    internal class Controllore
    {
        // Implementazione futura del controllore del gioco Sudoku

        public Controllore() {}

        public int[][] matrice;
        public void InizializzaMatrice()
        {
            matrice = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                matrice[i] = new int[9];
            }
        }

        public void ControllaSudoku()
        {
            //controllo per righe
            for (int i = 0; i < 9; i++)
            {
                matrice[i][0] = 0;
            }

            for (int i = 0;i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                    Console.Write(matrice[i][j] + " ");
                Console.WriteLine("\n");
            }
        }
    }
}
