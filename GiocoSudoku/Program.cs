using DocumentFormat.OpenXml.Drawing.ChartDrawing;

using System.Drawing;
using System.Runtime.InteropServices.JavaScript;

namespace GiocoSudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string[] opzioni = { "=> Gioca", "=> Esci" };
                int sceltaIndice = 0;
                bool selezionato = false;

                while (!selezionato)
                {
                    Console.Clear();    
                    Console.WriteLine("╔═══════════════════════════════╗");
                    Console.WriteLine("║      ═══ MENU SUDOKU ═══       ║");
                    Console.WriteLine("╚═══════════════════════════════╝");
                    Console.WriteLine();
                    Console.WriteLine("(Usa le frecce ↑↓ e premi ENTER per selezionare)");
                    Console.WriteLine();

                    for (int i = 0; i < opzioni.Length; i++)
                    {
                        if (i == sceltaIndice)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"  ► {opzioni[i]} ◄");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"    {opzioni[i]}");
                        }
                    }

                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        sceltaIndice = (sceltaIndice - 1 + opzioni.Length) % opzioni.Length;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        sceltaIndice = (sceltaIndice + 1) % opzioni.Length;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        selezionato = true;
                    }
                }

                if (sceltaIndice == 0)
                {
                    Console.Clear();
                    Griglia griglia = new Griglia();
                   // griglia.StringaGriglia();
                    griglia.Gioca();
                    Console.WriteLine("\nPremi un tasto per tornare al menu...");
                    Console.ReadKey(true);
                }
                else if (sceltaIndice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Arrivederci!");
                    break;
                }
            }
        }
    }
}
