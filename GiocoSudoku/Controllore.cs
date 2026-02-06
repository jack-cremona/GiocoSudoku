using System.Collections.Generic;
using System.Linq;

namespace GiocoSudoku
{
    /// <summary>
    /// Classe che gestisce la validazione del Sudoku
    /// </summary>
    internal class Controllore
    {
        /// <summary>
        /// Verifica se il Sudoku è completamente risolto e valido
        /// </summary>
        /// <param name="matrice">La matrice 9x9 di caselle del Sudoku</param>
        /// <returns>True se il Sudoku è completo e valido, False altrimenti</returns>
        public static bool IsValidSudoku(Casella[,] matrice)
        {
            // 1. Controllo che non ci siano celle vuote (per la vittoria)
            // Se trova una cella con valore 0, il Sudoku non è completo
            foreach (var c in matrice) if (c.Valore == 0) return false;

            // 2. Controllo Righe, Colonne e Quadranti per duplicati
            // Verifica che ogni riga, colonna e quadrante 3x3 non contenga numeri duplicati
            for (int i = 0; i < 9; i++)
            {
                if (!IsSequenceValid(GetRiga(matrice, i)) ||
                    !IsSequenceValid(GetColonna(matrice, i)) ||
                    !IsSequenceValid(GetSquare(matrice, i)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se il Sudoku parzialmente compilato è valido
        /// (non controlla se è completo, solo se non ci sono errori)
        /// </summary>
        /// <param name="matrice">La matrice 9x9 di caselle del Sudoku</param>
        /// <returns>True se non ci sono duplicati in righe, colonne o quadranti, False altrimenti</returns>
        public static bool IsValidPartial(Casella[,] matrice)
        {
            // Controlla tutte le 9 righe, 9 colonne e 9 quadranti
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

        /// <summary>
        /// Verifica che una sequenza di numeri non contenga duplicati (ignora gli zeri)
        /// </summary>
        /// <param name="sequence">La sequenza di numeri da validare</param>
        /// <returns>True se non ci sono duplicati (esclusi gli zeri), False altrimenti</returns>
        private static bool IsSequenceValid(IEnumerable<int> sequence)
        {
            // Filtra gli zeri (celle vuote) e ottiene solo i valori inseriti
            var nums = sequence.Where(n => n != 0).ToList();
            // Confronta il numero di elementi con il numero di elementi distinti
            // Se sono uguali, non ci sono duplicati
            return nums.Count == nums.Distinct().Count();
        }

        /// <summary>
        /// Estrae i valori di una riga specifica dalla matrice
        /// </summary>
        /// <param name="m">La matrice del Sudoku</param>
        /// <param name="r">L'indice della riga (0-8)</param>
        /// <returns>Una sequenza dei valori contenuti nella riga</returns>
        private static IEnumerable<int> GetRiga(Casella[,] m, int r)
        {
            // Itera su tutte le 9 colonne della riga specificata
            for (int c = 0; c < 9; c++) yield return m[r, c].Valore;
        }

        /// <summary>
        /// Estrae i valori di una colonna specifica dalla matrice
        /// </summary>
        /// <param name="m">La matrice del Sudoku</param>
        /// <param name="c">L'indice della colonna (0-8)</param>
        /// <returns>Una sequenza dei valori contenuti nella colonna</returns>
        private static IEnumerable<int> GetColonna(Casella[,] m, int c)
        {
            // Itera su tutte le 9 righe della colonna specificata
            for (int r = 0; r < 9; r++) yield return m[r, c].Valore;
        }

        /// <summary>
        /// Estrae i valori di un quadrante 3x3 specifico dalla matrice
        /// </summary>
        /// <param name="m">La matrice del Sudoku</param>
        /// <param name="k">L'indice del quadrante (0-8, numerati da sinistra a destra, dall'alto in basso)</param>
        /// <returns>Una sequenza dei valori contenuti nel quadrante</returns>
        private static IEnumerable<int> GetSquare(Casella[,] m, int k)
        {
            // Calcola la riga di partenza del quadrante (0, 3 o 6)
            int rStart = (k / 3) * 3;
            // Calcola la colonna di partenza del quadrante (0, 3 o 6)
            int cStart = (k % 3) * 3;
            // Itera su tutte le 9 celle del quadrante 3x3
            for (int r = rStart; r < rStart + 3; r++)
                for (int c = cStart; c < cStart + 3; c++)
                    yield return m[r, c].Valore;
        }
    }
}