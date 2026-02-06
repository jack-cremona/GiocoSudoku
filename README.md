# GiocoSudoku

**GiocoSudoku** è un'applicazione console interattiva sviluppata in **C#** che permette di giocare a Sudoku o di osservare un algoritmo di risoluzione automatica (Bot) in azione. Il progetto utilizza **Spectre.Console** per offrire un'interfaccia terminale moderna e intuitiva.

## Caratteristiche

* **Modalità Giocatore**: Navigazione della griglia con le frecce direzionali e inserimento numeri in tempo reale.
* **Risolutore Automatico (Bot)**: Algoritmo di **backtracking** capace di risolvere il puzzle partendo da qualsiasi configurazione valida.
* **Validazione Intelligente**: Sistema di controllo che identifica duplicati in righe, colonne e quadranti 3x3.
* **Interfaccia Avanzata**: Menu a selezione interattiva e feedback visivo colorato per errori e successi.

## Tecnologie Utilizzate

* **.NET 10**
* **Spectre.Console**: Per la grafica e i menu nel terminale.
* **LINQ**: Per una validazione efficiente della logica di gioco.

## Struttura del Codice

Il progetto segue una chiara separazione delle responsabilità:

* `Program.cs`: Gestisce il menu principale e il flusso dell'applicazione.
* `Griglia.cs`: Contiene la logica di gioco, la gestione dell'input utente e l'algoritmo del Bot.
* `Controllore.cs`: Isola la logica di validazione delle regole del Sudoku.
* `Casella.cs`: Definisce lo stato di ogni singola cella (valore, posizione e modificabilità).

## Come Giocare

1.  **Movimento**: Usa le `Frecce Direzionali` per spostare il cursore (evidenziato in giallo).
2.  **Inserimento**: Digita i numeri da `1` a `9` per riempire le caselle verdi.
3.  **Cancellazione**: Digita `0` per svuotare una cella inserita.
4.  **Controllo Manuale**: Premi `C` per verificare se la configurazione attuale contiene errori.
5.  **Uscita**: Premi `E` per tornare al menu principale.

### Legenda Colori
* **Ciano**: Numeri iniziali del puzzle (non modificabili).
* **Verde**: Numeri inseriti dal giocatore.
* **Giallo**: Posizione attuale del cursore.