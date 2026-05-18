using Helpers;

namespace FactureTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string mdp = "toto1234";
            string mdpChiffre = Security.Hash(mdp);
            Console.WriteLine($"--->{mdpChiffre}");
            bool ok = Security.Verify(mdp, mdpChiffre);
            Console.WriteLine(ok);
        }
    }
}
