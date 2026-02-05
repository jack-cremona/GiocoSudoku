using System;

namespace GiocoSudoku
{
    internal class Griglia
    {
        public Casella[,] Matrice = new Casella[9, 9];
        private int[] valoriIniziali = {
            5, 3, 0, 0, 7, 0, 0, 0, 0, 6, 0, 0, 1, 9, 5, 0, 0, 0, 0, 9, 8, 0, 0, 0, 0, 6, 0, 8, 0, 0, 0, 6, 0, 0, 0, 3, 4, 0, 0, 8, 0, 3, 0, 0, 1, 7, 0, 0, 0, 2, 0, 0, 0, 6, 0, 6, 0, 0, 0, 0, 2, 8, 0, 0, 0, 0, 4, 1, 9, 0, 0, 5, 0, 0, 0, 0, 8, 0, 0, 7, 9
        };

        public Griglia()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    Matrice[i, j] = new Casella(valoriIniziali[i * 9 + j], i, j);
        }

        public void Gioca()
        {
            int r = 0, c = 0;
            string messaggioFeedback = "";

            while (true)
            {
                Console.Clear();
                StampaGriglia(r, c);

                // Mostra eventuale messaggio di feedback dal controllo manuale
                if (!string.IsNullOrEmpty(messaggioFeedback))
                {
                    Console.WriteLine(messaggioFeedback);
                    messaggioFeedback = ""; // Reset dopo la stampa
                }

                var key = Console.ReadKey(true);

                // Movimento
                if (key.Key == ConsoleKey.UpArrow) r = (r - 1 + 9) % 9;
                else if (key.Key == ConsoleKey.DownArrow) r = (r + 1) % 9;
                else if (key.Key == ConsoleKey.LeftArrow) c = (c - 1 + 9) % 9;
                else if (key.Key == ConsoleKey.RightArrow) c = (c + 1) % 9;

                // Inserimento numeri
                else if (key.KeyChar >= '0' && key.KeyChar <= '9')
                {
                    if (Matrice[r, c].Modificabile)
                        Matrice[r, c].Valore = int.Parse(key.KeyChar.ToString());

                    // Controllo automatico vittoria (griglia piena e corretta)
                    if (Controllore.IsValidSudoku(Matrice))
                    {
                        Console.Clear();
                        StampaGriglia(-1, -1);
                        Console.WriteLine("\n🎉 COMPLIMENTI! HAI VINTO!");
                        break;
                    }
                }
                // TASTO PER IL CONTROLLO MANUALE
                else if (key.Key == ConsoleKey.C)
                {
                    if (Controllore.IsValidPartial(Matrice))
                    {
                        messaggioFeedback = "✅ Al momento non ci sono errori!";
                    }
                    else
                    {
                        Console.Beep();
                        messaggioFeedback = "❌ Attenzione: ci sono dei numeri duplicati!";
                    }
                }
                else if (key.Key == ConsoleKey.E) break;
            }
        }

        public void Bot()
        {
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

        private bool Risolvi(int r, int c)
        {
            if (r == 9) return true;
            int nextR = (c == 8) ? r + 1 : r;
            int nextC = (c == 8) ? 0 : c + 1;

            if (Matrice[r, c].Valore != 0) return Risolvi(nextR, nextC);

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(r, c, num))
                {
                    Matrice[r, c].Valore = num;
                    if (Risolvi(nextR, nextC)) return true;
                    Matrice[r, c].Valore = 0;
                }
            }
            return false;
        }

        private bool IsSafe(int r, int c, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (Matrice[r, i].Valore == num || Matrice[i, c].Valore == num) return false;
            }
            int rs = (r / 3) * 3, cs = (c / 3) * 3;
            for (int i = rs; i < rs + 3; i++)
                for (int j = cs; j < cs + 3; j++)
                    if (Matrice[i, j].Valore == num) return false;
            return true;
        }

        private void StampaGriglia(int curR, int curC)
        {
            Console.WriteLine("╔═══════╦═══════╦═══════╗");
            for (int i = 0; i < 9; i++)
            {
                Console.Write("║ ");
                for (int j = 0; j < 9; j++)
                {
                    if (i == curR && j == curC)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = Matrice[i, j].Modificabile ? ConsoleColor.Green : ConsoleColor.Cyan;
                    }

                    string v = Matrice[i, j].Valore == 0 ? "." : Matrice[i, j].Valore.ToString();
                    Console.Write(v + " ");
                    Console.ResetColor();

                    if ((j + 1) % 3 == 0 && j < 8) Console.Write("║ ");
                }
                Console.WriteLine("║");
                if ((i + 1) % 3 == 0 && i < 8) Console.WriteLine("╠═══════╬═══════╬═══════╣");
            }
            Console.WriteLine("╚═══════╩═══════╩═══════╝");

            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║  FRECCE: Muoviti   │ NUMERI: Inserisci ║");
            Console.WriteLine("║  0: Cancella       │ C: Controlla      ║"); // <--- AGGIUNTO C
            Console.WriteLine("║  E: Esci al Menu   │                   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }
    }
}