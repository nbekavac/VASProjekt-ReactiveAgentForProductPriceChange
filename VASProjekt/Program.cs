
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Linq;
using System.Data;
using ActressMas;

namespace VASProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            EnvironmentMas environment = new EnvironmentMas(parallel: false, randomOrder: false);
            


            Console.Write("Mobile model: ");
            string mobile_model = Console.ReadLine();
            Console.Write("Mobile color: ");
            string mobile_color = Console.ReadLine();
            Console.WriteLine("--------------------------");
           
            SenderAgent senderAgent = new SenderAgent(mobile_model, mobile_color);
            environment.Add(senderAgent, "senderAgent");

            ReceivingAgent receivingAgent = new ReceivingAgent();
            environment.Add(receivingAgent, "receivingAgent");

            environment.Start();



            Console.WriteLine("--------------------------");
            Console.Write("Is the product on your wanted price(Yes/No?)");
            string responseOnWantedPrice = Console.ReadLine();
            string responseOnPriceNotifications;
            string wantedPrice;

            EnvironmentMas environment1 = new EnvironmentMas(parallel: false, randomOrder: false);
            

            if (responseOnWantedPrice == "No" || responseOnWantedPrice == "no" || responseOnWantedPrice == "NO")
            {
                Console.Write("Do you want to receive the notifications when the price is lower(Yes/No)? ");
                responseOnPriceNotifications = Console.ReadLine();
                if (responseOnPriceNotifications == "Yes" || responseOnPriceNotifications == "yes" || responseOnPriceNotifications == "YES")
                {
                    Console.Write("What is your wanted price in EUR? ");
                    wantedPrice= Console.ReadLine();
                    PriceAgent priceAgent = new PriceAgent(mobile_model, mobile_color, wantedPrice);
                    environment1.Add(priceAgent, "priceAgent");
                    environment1.Start();
                }
                else
                {
                    Console.WriteLine("You will not receive notifications when the price is lower");
                }
            }
            else
            {
                Console.WriteLine("You can go on Ekupi page and buy your phone");
            }
           
            Console.ReadLine();
        }
    }
}

