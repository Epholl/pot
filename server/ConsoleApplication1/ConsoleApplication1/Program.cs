﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerServiceClient client = new ServerServiceClient();
            int i = 1;
            string input = "";
            while (input == "")
            {
                Console.WriteLine("Okay, here we go with " + i + ": " + client.GetSampleData(i++));
                input = Console.ReadLine();
            }
            client.Close();
        }
    }
}
