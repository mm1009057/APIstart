using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args) //async to prossess away from main
    {
        int namesCount = 1;
        string oldestName = "Null";
        int oldestAge =  0;
        do
        {
            Console.Write("Enter a name>> ");
            string name = Console.ReadLine();
            try
            {
                using HttpClient client = new();
                string url = $"https://api.agify.io?name={name}"; //API Link
                HttpResponseMessage response = await client.GetAsync(url);

                string jsonResponse = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(jsonResponse);
                string age = json["age"]?.ToString();
                string count = json["count"]?.ToString();

                namesCount++;
                Console.WriteLine($"Name {namesCount -1} is aproximatly: {age}");

                
                if(Convert.ToInt32(age) > oldestAge)
                {
                    oldestAge = Convert.ToInt32(age);
                    oldestName = name;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"IO Error: {e}");
            }
        }
        while (namesCount < 4); //3 names
        Console.WriteLine($"{oldestName} is the oldest");
     }
}





