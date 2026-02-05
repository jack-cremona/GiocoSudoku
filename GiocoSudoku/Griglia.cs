using System;
using System.Text;

namespace GiocoSudoku
{
    internal class Griglia
    {
        public Casella[,] Matrice = new Casella[9, 9];

        private int[] valori =
        {
            5, 3, 0, 0, 7, 0, 0, 0, 0, 6, 0, 0, 1, 9, 5, 0, 0, 0, 0, 9, 8, 0, 0, 0, 0, 6, 0, 8, 0, 0, 0, 6, 0, 0, 0, 3, 4, 0, 0, 8, 0, 3, 0, 0, 1, 7, 0, 0, 0, 2, 0, 0, 0, 6, 0, 6, 0, 0, 0, 0, 2, 8, 0, 0, 0, 0, 4, 1, 9, 0, 0, 5, 0, 0, 0, 0, 8, 0, 0, 7, 9
        };

        public Griglia()
        {
            // Usa cicli for classici per gestire gli indici da 0 a 8
            for (int i = 0; i < 9; i++) // Riga
            {
                for (int j = 0; j < 9; j++) // Colonna
                {
                    // Calcola l'indice lineare: (riga * 9) + colonna
                    int indiceLineare = i * 9 + j;
                    int valoreCorrente = valori[indiceLineare];

                    Matrice[i, j] = new Casella(valoreCorrente, i, j);
                }
            }
        }

        public void StringaGriglia()
        {
            
            int[,] matrix = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrix[i,j]= Matrice[i,j].GetCasella();
                }
            }
            
{
    Console.WriteLine("╔═══════╦═══════╦═══════╗");
    for (int i = 0; i < 9; i++)
    {
        Console.Write("║ ");
        for (int j = 0; j < 9; j++)
        {
            // Sostituisce lo 0 con un punto per leggibilità, altrimenti stampa il numero
            string val = matrix[i, j] == 0 ? "-" : matrix[i, j].ToString();

            // Colora i numeri diversi da zero (opzionale, per estetica)
            if (val != ".") Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(val + " ");
            Console.ResetColor();

            // Aggiunge divisore verticale ogni 3 colonne
            if ((j + 1) % 3 == 0 && j < 8) Console.Write("║ ");
        }
        Console.Write("║");
        Console.WriteLine();

        // Aggiunge divisore orizzontale ogni 3 righe (ma non l'ultima)
        if ((i + 1) % 3 == 0 && i < 8)
        {
            Console.WriteLine("╠═══════╬═══════╬═══════╣");
        }
    }
    Console.WriteLine("╚═══════╩═══════╩═══════╝");
}
           

        }
    }
}
