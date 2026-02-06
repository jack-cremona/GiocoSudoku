using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Extensions.CommandLineUtils;
using System;
using Spectre.Console;
using AnsiConsole = Spectre.Console.AnsiConsole;

namespace GiocoSudoku
{
    /// <summary>
    /// Rappresenta la griglia del gioco Sudoku e gestisce la logica di gioco
    /// </summary>
    internal class Griglia
    {
        /// <summary>
        /// La matrice 9x9 che contiene tutte le caselle del Sudoku
        /// </summary>
        public Casella[,] Matrice = new Casella[9, 9];

        /// <summary>
        /// Array con i valori iniziali del puzzle Sudoku (0 = casella vuota)
        /// Rappresenta il Sudoku in formato lineare (81 elementi)
        /// </summary>
        private int[] valoriIniziali = {
            5, 3, 0, 0, 7, 0, 0, 0, 0,
            6, 0, 0, 1, 9, 5, 0, 0, 0,
            0, 9, 8, 0, 0, 0, 0, 6, 0,
            8, 0, 0, 0, 6, 0, 0, 0, 3, 
            4, 0, 0, 8, 0, 3, 0, 0, 1,
            7, 0, 0, 0, 2, 0, 0, 0, 6,
            0, 6, 0, 0, 0, 0, 2, 8, 0,
            0, 0, 0, 4, 1, 9, 0, 0, 5,
            0, 0, 0, 0, 8, 0, 0, 7, 9
        };

        /// <summary>
        /// Costruttore che inizializza la griglia con i valori predefiniti
        /// </summary>
        public Griglia()
        {
            // Popola la matrice 9x9 con le caselle, convertendo l'array lineare in matrice
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    Matrice[i, j] = new Casella(valoriIniziali[i * 9 + j], i, j);
        }

        /// <summary>
        /// Gestisce la modalità di gioco interattiva per il giocatore umano
        /// </summary>
        public void Gioca()
        {
            bool isExit = false;
            int r = 0, c = 0; // Posizione corrente del cursore
            string messaggioFeedback = ""; // Messaggio da mostrare dopo il controllo

            // Loop principale del gioco
            while (isExit == false)
            {
                Console.Clear();
                StampaGriglia(r, c);

                // Mostra eventuale messaggio di feedback dal controllo manuale
                if (!string.IsNullOrEmpty(messaggioFeedback))
                {
                    // Mostra messaggi di errore in rosso
                    if(messaggioFeedback.Contains("Attenzione") || messaggioFeedback.Contains("duplicati"))
                    {
                        AnsiConsole.MarkupLine($"[red]{messaggioFeedback}[/]");
                    }
                    // Mostra messaggi positivi in verde
                    else
                    {
                        AnsiConsole.MarkupLine($"[green]{messaggioFeedback}[/]");
                    }
                    messaggioFeedback = ""; // Reset dopo la stampa
                }

                var key = Console.ReadKey(true);

                // Gestione movimento del cursore con le frecce direzionali
                // L'operazione % 9 assicura che il cursore torni dall'altra parte quando raggiunge i bordi
                if (key.Key == ConsoleKey.UpArrow) r = (r - 1 + 9) % 9;
                else if (key.Key == ConsoleKey.DownArrow) r = (r + 1) % 9;
                else if (key.Key == ConsoleKey.LeftArrow) c = (c - 1 + 9) % 9;
                else if (key.Key == ConsoleKey.RightArrow) c = (c + 1) % 9;

                // Inserimento numeri (0-9)
                else if (key.KeyChar >= '0' && key.KeyChar <= '9')
                {
                    // Permette l'inserimento solo se la casella è modificabile
                    if (Matrice[r, c].Modificabile)
                        Matrice[r, c].Valore = int.Parse(key.KeyChar.ToString());

                    // Controllo automatico vittoria (griglia piena e corretta)
                    if (Controllore.IsValidSudoku(Matrice))
                    {
                        Console.Clear();
                        StampaGriglia(-1, -1); // Stampa senza evidenziare alcuna cella
                        AnsiConsole.MarkupLine("[green]Congratulazioni! Hai risolto il Sudoku![/]");
                        break;
                    }
                }
                // TASTO C PER IL CONTROLLO MANUALE
                else if (key.Key == ConsoleKey.C)
                {
                    // Verifica se ci sono errori nella configurazione attuale
                    if (Controllore.IsValidPartial(Matrice))
                    {
                        messaggioFeedback = "Al momento non ci sono errori!";
                    }
                    else
                    {
                        messaggioFeedback = "Attenzione: ci sono dei numeri duplicati!";
                    }
                }
                // TASTO E PER USCIRE
                else if (key.Key == ConsoleKey.E)
                {
                    Console.Clear();
                    isExit = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Avvia la risoluzione automatica del Sudoku tramite backtracking
        /// </summary>
        public void Bot()
        {
            // Tenta di risolvere il Sudoku partendo dalla posizione (0,0)
            if (Risolvi(0, 0))
            {
                Console.Clear();
                StampaGriglia(-1, -1);
                Console.WriteLine("\nSudoku risolto dal Bot!");
            }
            else
            {
                Console.WriteLine("\nImpossibile risolvere questo Sudoku.");
            }
        }

        /// <summary>
        /// Risolve il Sudoku ricorsivamente usando l'algoritmo di backtracking
        /// </summary>
        /// <param name="r">Indice della riga corrente</param>
        /// <param name="c">Indice della colonna corrente</param>
        /// <returns>True se il Sudoku è stato risolto, False altrimenti</returns>
        private bool Risolvi(int r, int c)
        {
            // Caso base: se abbiamo superato l'ultima riga, il Sudoku è risolto
            if (r == 9) return true;

            // Calcola la prossima posizione (si muove da sinistra a destra, poi alla riga successiva)
            int nextR = (c == 8) ? r + 1 : r;
            int nextC = (c == 8) ? 0 : c + 1;

            // Se la casella è già riempita, passa alla successiva
            if (Matrice[r, c].Valore != 0) return Risolvi(nextR, nextC);

            // Prova tutti i numeri da 1 a 9
            for (int num = 1; num <= 9; num++)
            {
                // Verifica se il numero può essere inserito in questa posizione
                if (IsSafe(r, c, num))
                {
                    Matrice[r, c].Valore = num; // Prova questo numero
                    if (Risolvi(nextR, nextC)) return true; // Continua con la prossima casella
                    Matrice[r, c].Valore = 0; // Backtrack: rimuove il numero se non porta a soluzione
                }
            }
            return false; // Nessun numero valido trovato
        }

        /// <summary>
        /// Verifica se è sicuro inserire un numero in una specifica posizione
        /// </summary>
        /// <param name="r">Indice della riga</param>
        /// <param name="c">Indice della colonna</param>
        /// <param name="num">Il numero da verificare (1-9)</param>
        /// <returns>True se il numero può essere inserito senza violare le regole del Sudoku</returns>
        private bool IsSafe(int r, int c, int num)
        {
            // Controlla riga e colonna per duplicati
            for (int i = 0; i < 9; i++)
            {
                if (Matrice[r, i].Valore == num || Matrice[i, c].Valore == num) return false;
            }

            // Controlla il quadrante 3x3
            int rs = (r / 3) * 3, cs = (c / 3) * 3; // Calcola l'angolo superiore sinistro del quadrante
            for (int i = rs; i < rs + 3; i++)
                for (int j = cs; j < cs + 3; j++)
                    if (Matrice[i, j].Valore == num) return false;

            return true; // Il numero può essere inserito
        }

        /// <summary>
        /// Stampa la griglia del Sudoku sulla console con formattazione grafica
        /// </summary>
        /// <param name="curR">Riga corrente del cursore (-1 per non evidenziare)</param>
        /// <param name="curC">Colonna corrente del cursore (-1 per non evidenziare)</param>
        private void StampaGriglia(int curR, int curC)
        {
            // Stampa il bordo superiore della griglia
            Console.WriteLine("╔═══════╦═══════╦═══════╗");

            for (int i = 0; i < 9; i++)
            {
                Console.Write("║ ");
                for (int j = 0; j < 9; j++)
                {
                    // Evidenzia la casella corrente con sfondo giallo
                    if (i == curR && j == curC)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        // Verde per caselle modificabili, Ciano per numeri iniziali
                        Console.ForegroundColor = Matrice[i, j].Modificabile ? ConsoleColor.Green : ConsoleColor.Cyan;
                    }

                    // Mostra un punto per le caselle vuote, altrimenti il numero
                    string v = Matrice[i, j].Valore == 0 ? "." : Matrice[i, j].Valore.ToString();
                    Console.Write(v + " ");
                    Console.ResetColor();

                    // Separatore verticale tra i quadranti 3x3
                    if ((j + 1) % 3 == 0 && j < 8) Console.Write("║ ");
                }
                Console.WriteLine("║");

                // Separatore orizzontale tra i quadranti 3x3
                if ((i + 1) % 3 == 0 && i < 8) Console.WriteLine("╠═══════╬═══════╬═══════╣");
            }

            // Stampa il bordo inferiore della griglia
            Console.WriteLine("╚═══════╩═══════╩═══════╝");

            // Stampa la legenda dei comandi
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║  FRECCE: Muoviti   │ NUMERI: Inserisci ║");
            Console.WriteLine("║  0: Cancella       │ C: Controlla      ║");
            Console.WriteLine("║  E: Esci al Menu   │                   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }
    }
}