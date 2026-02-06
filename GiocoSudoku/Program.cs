using System;
using Spectre.Console;

namespace GiocoSudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                var banner = new FigletText("MENU SUDOKU")
                {
                    Color = Color.White,
                    Justification = Justify.Center
                };

                AnsiConsole.Write(banner);

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleziona un'opzione:")
                        .AddChoices("Gioca", "Esci", "Fai giocare il bot"));

                if (choice == "Esci")
                    break;

                Griglia g = new Griglia();
                
                if (choice == "Gioca") 
                    g.Gioca();
                else if (choice == "Fai giocare il bot") 
                    g.Bot();

                Console.WriteLine("\nPremi un tasto per tornare al menu...");
                Console.ReadKey();
            }
        }
    }
}