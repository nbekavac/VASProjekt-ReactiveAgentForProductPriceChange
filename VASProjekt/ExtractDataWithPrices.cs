using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace VASProjekt
{
    public class ExtractDataWithPrices
    {
        private int _wantedPrice;
        private int _price;
        private string priceInString;
        private string ReadConfigJsonFile()
        {
            string config = string.Empty;
            using (StreamReader r = new StreamReader("configWithPrices.json"))
            {
                config = r.ReadToEnd();
            }
            return config;
        }

        public string ExtractingDataFromFile(string mobile_model, string mobile_color, string wantedPrice)
        {
            
            _wantedPrice= int.Parse(wantedPrice);
            string config = ReadConfigJsonFile();
            dynamic json_file = JsonConvert.DeserializeObject(config);
            foreach (var phone in json_file["phones"])
            {
                if (phone["model"].Value == mobile_model && phone["color"].Value == mobile_color)
                {                  
                    priceInString = phone["price"].Value;
                    _price = int.Parse(priceInString);
                    if (_price<=_wantedPrice)
                    {
                        Console.Write("The price has reached your wanted value or less and that value is: ");
                        Console.Write(_price);
                        Console.WriteLine(". You can go on Ekupi page and buy your phone");
                        return priceInString;
                    }                
                }
            }
            return null;
        }
    }
}
