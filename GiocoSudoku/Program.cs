using System;
using Spectre.Console;

namespace GiocoSudoku
{
    /// <summary>
    /// Classe principale del programma che gestisce il menu e l'avvio del gioco
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione
        /// </summary>
        /// <param name="args">Argomenti della riga di comando</param>
        static void Main(string[] args)
        {
            // Loop infinito per mantenere il menu attivo fino all'uscita
            while (true)
            {
                Console.Clear();

                // Crea e visualizza il banner del titolo con Spectre.Console
                var banner = new FigletText("MENU SUDOKU")
                {
                    Color = Color.White,
                    Justification = Justify.Center
                };

                AnsiConsole.Write(banner);

                // Mostra il menu di selezione con le opzioni disponibili
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleziona un'opzione:")
                        .AddChoices("Gioca", "Esci", "Fai giocare il bot"));

                // Se l'utente sceglie di uscire, termina il programma
                if (choice == "Esci")
                    break;

                // Crea una nuova istanza della griglia Sudoku
                Griglia g = new Griglia();

                // Avvia la modalità di gioco selezionata
                if (choice == "Gioca") 
                    g.Gioca(); // Modalità giocatore umano
                else if (choice == "Fai giocare il bot") 
                    g.Bot(); // Modalità risoluzione automatica

                // Attende un input prima di tornare al menu
                Console.WriteLine("\nPremi un tasto per tornare al menu...");
                Console.ReadKey();
            }
        }
    }
}