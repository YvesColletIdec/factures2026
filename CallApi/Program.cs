using FactureEntities.Entities;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CallApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://localhost:7016/api/ArticleApi/Toto?id=1440";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            int i = 0;
            i = 1;
            var x = JsonConvert.DeserializeObject<dynamic>(response);
            Console.WriteLine(x.description);
            var z = JsonConvert.DeserializeObject<Article>(response);
            Console.WriteLine(z.Prix);
        }
    }
}
