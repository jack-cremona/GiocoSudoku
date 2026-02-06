using System;
using System.Collections.Generic;
using System.Text;

namespace GiocoSudoku
{
    /// <summary>
    /// Rappresenta una singola casella della griglia Sudoku
    /// </summary>
    internal class Casella
    {
        /// <summary>
        /// Il valore numerico contenuto nella casella (0 = vuota, 1-9 = numero inserito)
        /// </summary>
        public int Valore {  get; set; }

        /// <summary>
        /// Indica se la casella può essere modificata dal giocatore
        /// (false per i numeri iniziali, true per le caselle vuote)
        /// </summary>
        public bool Modificabile {  get; set; }

        /// <summary>
        /// L'indice della riga in cui si trova la casella (0-8)
        /// </summary>
        public int riga { get; set; }

        /// <summary>
        /// L'indice della colonna in cui si trova la casella (0-8)
        /// </summary>
        public int colonna { get; set; }

        /// <summary>
        /// Costruttore della casella
        /// </summary>
        /// <param name="valore">Il valore iniziale (0 per casella vuota, 1-9 per numero predefinito)</param>
        /// <param name="riga">L'indice della riga (0-8)</param>
        /// <param name="colonna">L'indice della colonna (0-8)</param>
        public Casella(int valore, int riga, int colonna)
        {
            Valore = valore;
            // Se il valore è 0 (casella vuota), la casella è modificabile
            if (valore == 0)
            {
                Modificabile = true;
            }
            // Altrimenti è un numero iniziale del puzzle e non può essere modificato
            else
            {
                Modificabile = false;
            }

            this.riga = riga;
            this.colonna = colonna;
        }

        /// <summary>
        /// Restituisce il valore corrente della casella
        /// </summary>
        /// <returns>Il valore della casella (0-9)</returns>
        public int GetCasella()
        {
            return Valore;

        }
    }
}
