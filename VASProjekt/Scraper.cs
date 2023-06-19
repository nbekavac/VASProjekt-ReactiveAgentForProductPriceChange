using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VASProjekt
{
    public class Scraper
    {
        private string ReadConfigJsonFile()
        {
            string config = string.Empty;
            using (StreamReader r = new StreamReader("config.json"))
            {
                config = r.ReadToEnd();
            }
            return config;
        }

        private string ExtractingDataFromFile(string mobile_model, string mobile_color)
        {
            string config = ReadConfigJsonFile();
            dynamic json_file = JsonConvert.DeserializeObject(config);
            foreach (var phone in json_file["phones"])
            {
                if (phone["model"].Value == mobile_model && phone["color"].Value == mobile_color)
                {
                    string company = phone["company"].Value;
                    string model = phone["model"].Value;
                    string mobile_id = phone["id"].Value;
                    string display_size = phone["display_size"].Value;
                    string storage = phone["storage"].Value.Replace(" ", "");
                    string color = phone["color"].Value;
                    return $"{company},{model},{mobile_id},{display_size},{storage},{color}";
                }
            }
            Console.WriteLine("Mobile model and color not found");
            return null;
        }

        private string CreateUrl(string mobile_model, string mobile_color)
        {
            string data = ExtractingDataFromFile(mobile_model, mobile_color);
            string[] dataArray = data.Split(',');
            string company = dataArray[0];
            string model = dataArray[1];
            string mobile_id = dataArray[2];
            string display_size = dataArray[3];
            string storage = dataArray[4];
            string color = dataArray[5];

            string url_string_first_part = "https://www.ekupi.hr/hr/Elektronika/Mobiteli-i-dodaci/Mobiteli/";
            string url_string_middle_part = $"{company}-{model}-{storage}--{color}";
            url_string_middle_part = $"%2C-mobitel/p/{mobile_id}";
            string url_string_final = $"{url_string_first_part}{url_string_middle_part}{url_string_middle_part}";
            return url_string_final;
        }

        private HtmlDocument UrlRequest(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        private List<string> GetProductAttributes(string mobile_model, string mobile_color)
        {
            string url = CreateUrl(mobile_model, mobile_color);
            HtmlDocument data = UrlRequest(url);

            string product_data_name = data.DocumentNode.SelectSingleNode("//h1[contains(@class, 'name')]").InnerText;
            string product_name = product_data_name.Split(',')[0].Trim();
            List<string> product_list = new List<string>();

            string product_price = data.DocumentNode.SelectSingleNode("//dd[contains(@class, 'final-price')]").InnerText;
            string[] product_price_split = product_price.Split().Select(x => x.Trim()).ToArray();
            List<string> product_price_split_without_blanks = new List<string>();
            foreach (string product in product_price_split)
            {
                if (!string.IsNullOrEmpty(product))
                {
                    product_price_split_without_blanks.Add(product);
                }
            }
            product_list.AddRange(new string[] { "Product name ", product_name, "Price in euro ", product_price_split_without_blanks[0], "Price in kn ", product_price_split_without_blanks[2] });

            return product_list;
        }

        public void GetProductFrame(string mobile_model, string mobile_color)
        {
            List<string> data1 = GetProductAttributes(mobile_model, mobile_color);
            Console.WriteLine("Current price:");
            Console.WriteLine("--------------------------");
            foreach (string item in data1)
            {
                Console.WriteLine(item);
            }
        }
    }
}
