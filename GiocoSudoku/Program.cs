using System;

namespace GiocoSudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string[] opzioni = { "Gioca", "Esci", "Fai giocare il bot" };
                int scelta = 0;
                bool confermato = false;

                while (!confermato)
                {
                    Console.Clear();
                    Console.WriteLine("=== MENU SUDOKU ===\n");
                    for (int i = 0; i < opzioni.Length; i++)
                    {
                        Console.WriteLine(i == scelta ? $"> {opzioni[i]} <" : $"  {opzioni[i]}");
                    }

                    var k = Console.ReadKey(true).Key;
                    if (k == ConsoleKey.UpArrow) scelta = (scelta - 1 + 3) % 3;
                    else if (k == ConsoleKey.DownArrow) scelta = (scelta + 1) % 3;
                    else if (k == ConsoleKey.Enter) confermato = true;
                }

                Griglia g = new Griglia();
                if (scelta == 0) g.Gioca();
                else if (scelta == 2) g.Bot();
                else break;

                Console.WriteLine("\nPremi un tasto...");
                Console.ReadKey();
            }
        }
    }
}