using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServerServiceClient client = new ServerServiceClient();
                var result = client.getHighscoresForLevel("test1");
                Console.WriteLine(result.scores.Count());
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown: " + e.ToString());
            }
            Console.WriteLine("Press Enter key to exit");
            Console.ReadLine();
        }
    }
}
