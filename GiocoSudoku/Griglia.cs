using System;
using System.Text;

namespace GiocoSudoku
{
    internal class Griglia
    {
        public Casella[,] Matrice = new Casella[9, 9];

        private Controllore controllore;

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

            controllore = new Controllore();
        }

        public void Gioca()
        {
            int rigaCursore = 0;
            int colonnaCursore = 0;
            bool giocoAttivo = true;

            while (giocoAttivo)
            {
                Console.Clear();
                StampaGrigliaCursor(rigaCursore, colonnaCursore);
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║  Frecce: Naviga │ Numero: Inserisci   ║");
                Console.WriteLine("║  0: Cancella    │ E: Esci dal gioco   ║");
                Console.WriteLine("╚════════════════════════════════════════╝");

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    rigaCursore = (rigaCursore - 1 + 9) % 9;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    rigaCursore = (rigaCursore + 1) % 9;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    colonnaCursore = (colonnaCursore - 1 + 9) % 9;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    colonnaCursore = (colonnaCursore + 1) % 9;
                }
                else if (key.KeyChar >= '0' && key.KeyChar <= '9')
                {
                    int numero = int.Parse(key.KeyChar.ToString());

                    if (!Matrice[rigaCursore, colonnaCursore].Modificabile)
                    {
                        Console.Beep();
                        Console.WriteLine("\n❌ Questa cella non è modificabile!");
                        System.Threading.Thread.Sleep(1500);
                    }
                    else
                    {
                        Matrice[rigaCursore, colonnaCursore].Valore = numero;
                    }

                    if (Controllore.IsValidSudoku(Matrice))
                    {
                        Console.Clear();
                        StampaGriglia();
                        Console.WriteLine("\n╔════════════════════════════════════════╗");
                        Console.WriteLine("║      🎉 HAI VINTO! 🎉                  ║");
                        Console.WriteLine("║    Complimenti, hai risolto il Sudoku! ║");
                        Console.WriteLine("╚════════════════════════════════════════╝");
                        System.Threading.Thread.Sleep(2000);
                        giocoAttivo = false;
                    }
                }
                else if (key.Key == ConsoleKey.E)
                {
                    giocoAttivo = false;
                }
            }
        }

        private void StampaGrigliaCursor(int rigaCursore, int colonnaCursore)
        {
            Console.WriteLine("╔═══════╦═══════╦═══════╗");
            for (int i = 0; i < 9; i++)
            {
                Console.Write("║ ");
                for (int j = 0; j < 9; j++)
                {
                    int val = Matrice[i, j].Valore;
                    string valStr = val == 0 ? "-" : val.ToString();

                    if (i == rigaCursore && j == colonnaCursore)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write(valStr);
                        Console.ResetColor();
                    }
                    else if (val != 0 && !Matrice[i, j].Modificabile)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(valStr);
                        Console.ResetColor();
                    }
                    else if (val != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(valStr);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(valStr);
                    }

                    Console.Write(" ");

                    if ((j + 1) % 3 == 0 && j < 8) Console.Write("║ ");
                }
                Console.Write("║");
                Console.WriteLine();

                if ((i + 1) % 3 == 0 && i < 8)
                {
                    Console.WriteLine("╠═══════╬═══════╬═══════╣");
                }
            }
            Console.WriteLine("╚═══════╩═══════╩═══════╝");
        }

        private void StampaGriglia()
        {
            Console.WriteLine("╔═══════╦═══════╦═══════╗");
            for (int i = 0; i < 9; i++)
            {
                Console.Write("║ ");
                for (int j = 0; j < 9; j++)
                {
                    int val = Matrice[i, j].Valore;
                    Console.ForegroundColor = Matrice[i, j].Modificabile ? ConsoleColor.Green : ConsoleColor.Cyan;
                    Console.Write(val + " ");
                    Console.ResetColor();

                    if ((j + 1) % 3 == 0 && j < 8) Console.Write("║ ");
                }
                Console.Write("║");
                Console.WriteLine();

                if ((i + 1) % 3 == 0 && i < 8)
                {
                    Console.WriteLine("╠═══════╬═══════╬═══════╣");
                }
            }
            Console.WriteLine("╚═══════╩═══════╩═══════╝");
        }
           

    }
}
