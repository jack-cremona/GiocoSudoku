namespace GiocoSudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Controllore controllore = new Controllore();
            controllore.InizializzaMatrice();
            controllore.ControllaSudoku();
        }
    }
}
