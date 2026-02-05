using System;
using System.Collections.Generic;
using System.Text;

namespace GiocoSudoku
{
    internal class Controllore
    {
        // Implementazione futura del controllore del gioco Sudoku

        //Primo numero matricce = numero righe , secondo numero = numero colonne

        public Controllore() {}

        public static bool IsValidSudoku(int[,] matrice)
        {
            // 1. Controllo Righe e Colonne simultaneamente
            for (int i = 0; i < 9; i++)
            {
                if (!IsValidSequence(GetRiga(matrice, i)) || !IsValidSequence(GetColonna(matrice, i)))
                    return false;
            }

            // 2. Controllo Sottogriglie 3x3
            for (int riga = 0; riga < 9; riga += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    if (!IsValidSequence(GetSquare(matrice, riga, col)))
                        return false;
                }
            }

            return true;
        }

        // Verifica se una sequenza di 9 numeri ha duplicati (escludendo lo 0)
        private static bool IsValidSequence(IEnumerable<int> sequence)
        {
            var nums = sequence.Where(n => n != 0).ToList();
            return nums.Count == nums.Distinct().Count();
        }

        // Estrae una riga
        private static IEnumerable<int> GetRiga(int[,] matrice, int riga)
        {
            for (int col = 0; col < 9; col++) yield return matrice[riga, col];
        }

        // Estrae una colonna
        private static IEnumerable<int> GetColonna(int[,] matrice, int col)
        {
            for (int riga = 0; riga < 9; riga++) yield return matrice[riga, col];
        }

        // Estrae un quadrante 3x3
        private static IEnumerable<int> GetSquare(int[,] matrice, int startRiga, int startCol)
        {
            for (int riga = 0; riga < 3; riga++)
            {
                for (int col = 0; col < 3; col++)
                {
                    yield return matrice[startRiga + riga, startCol + col];
                }
            }
        }
    }
}
