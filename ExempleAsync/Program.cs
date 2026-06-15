using System.Runtime.CompilerServices;

namespace ExempleAsync
{
    internal class Program
    {
        static async Task Main()
        {
            Console.WriteLine("---DEBUT---");
            Random r = new Random();
            List<Task> liste = new List<Task>();
            for (int i = 0; i <= 30; i++)
            {
                int nombre = r.Next(1000, 3000);
                liste.Add(CoucouAsync(i, nombre));
            }
            await Task.WhenAll(liste);
            Console.WriteLine("---FIN---");
        }
        public static async Task<int> CoucouAsync(int i, int nombre)
        {
            Console.WriteLine($"début{i}");
            await Task.Delay(nombre);
            Console.WriteLine($"fin{i} ({nombre})");
            return nombre;
        }

        public static async Task SalutAsync()
        {
            HttpClient client = new HttpClient();
            await client.PostAsync("asdf", null);
            Console.WriteLine("salut1");
            //await Task.Delay(3000);
            Console.WriteLine("salut2");
        }
    }
}
