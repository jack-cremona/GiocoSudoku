using System.Collections.Generic;
using System.Linq;

namespace GiocoSudoku
{
    internal class Controllore
    {
        public static bool IsValidSudoku(Casella[,] matrice)
        {
            // 1. Controllo che non ci siano celle vuote (per la vittoria)
            foreach (var c in matrice) if (c.Valore == 0) return false;

            // 2. Controllo Righe, Colonne e Quadranti per duplicati
            for (int i = 0; i < 9; i++)
            {
                if (!IsSequenceValid(GetRiga(matrice, i)) ||
                    !IsSequenceValid(GetColonna(matrice, i)) ||
                    !IsSequenceValid(GetSquare(matrice, i)))
                    return false;
            }
            return true;
        }

        public static bool IsValidPartial(Casella[,] matrice)
        {
            for (int i = 0; i < 9; i++)
            {
                // Se una riga, colonna o quadrato ha duplicati, non è valido
                if (!IsSequenceValid(GetRiga(matrice, i)) ||
                    !IsSequenceValid(GetColonna(matrice, i)) ||
                    !IsSequenceValid(GetSquare(matrice, i)))
                    return false;
            }
            return true;
        }
        private static bool IsSequenceValid(IEnumerable<int> sequence)
        {
            var nums = sequence.Where(n => n != 0).ToList();
            return nums.Count == nums.Distinct().Count();
        }

        private static IEnumerable<int> GetRiga(Casella[,] m, int r)
        {
            for (int c = 0; c < 9; c++) yield return m[r, c].Valore;
        }

        private static IEnumerable<int> GetColonna(Casella[,] m, int c)
        {
            for (int r = 0; r < 9; r++) yield return m[r, c].Valore;
        }

        private static IEnumerable<int> GetSquare(Casella[,] m, int k)
        {
            int rStart = (k / 3) * 3;
            int cStart = (k % 3) * 3;
            for (int r = rStart; r < rStart + 3; r++)
                for (int c = cStart; c < cStart + 3; c++)
                    yield return m[r, c].Valore;
        }
    }
}