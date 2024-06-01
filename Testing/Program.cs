using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Testing
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://www.avito.ru/krasnoyarsk?cd=1&q=1530170100");
                response.EnsureSuccessStatusCode();
                IConfiguration config = Configuration.Default;
                IBrowsingContext context = BrowsingContext.New(config);
                var source = await response.Content.ReadAsStringAsync();
                IDocument document = await context.OpenAsync(req => req.Content(source));

                IEnumerable<IElement> adverts = document.All.Where(
                    tag => tag.LocalName == "div" &&
                    tag.ClassName == "iva-item-content-rejJg");
                foreach (var advert in adverts)
                {
                    Advert adv = new Advert(
                        advert.QuerySelectorAll("h3").First(tag => tag.GetAttribute("itemprop") == "name").TextContent,
                        "https://www.avito.ru/" + advert.QuerySelector("a").GetAttribute("href"),
                        advert.QuerySelector("strong").TextContent,
                        advert.QuerySelectorAll("p").First(tag => tag.ClassName == "styles-module-root-YczkZ " +
                            "styles-module-size_s-AGMw8 styles-module-size_s_compensated-UgWYW styles-module-size_s-_z7mI " +
                            "styles-module-ellipsis-a2Uq1 stylesMarningNormal-module-root-S7NIr stylesMarningNormal-module-paragraph-s-Yhr2e " +
                            "styles-module-noAccent-LowZ8 styles-module-root_bottom-G4JNz styles-module-margin-bottom_6-_aVZm").TextContent,
                        advert.QuerySelectorAll("li").First(tag =>
                        tag.GetAttribute("data-marker").Contains("slider-image/image")).GetAttribute("data-marker").Replace("slider-image/image-", "")
                        );
                   Console.WriteLine( adv.Photo );
                 
                }
                Console.ReadLine();

                
                
            }
            catch(HttpRequestException e) 
            {
                Console.WriteLine("\nException Caught!");
              
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.ReadLine();
        }

        //Bibinet
        #region
        static async Task GetBibinet()
        {
            try
            {
                HttpContent content = new StringContent("{\"ver_api\":\"v3\",\"ver2\":1,\"kind\":\"profi\",\"parts_per_page\":50,\"city\":38,\"raw_oem_code\":\" 1530170100\"}"); //TODO: Сделать динамический ввод номера
                HttpResponseMessage response = await client.PostAsync("https://bibinet.ru/service/search/v5/parts/", content);
                response.EnsureSuccessStatusCode();
                JsonSerializer jsonSerializer = new JsonSerializer();
                string responseBody = await response.Content.ReadAsStringAsync();
                Rootobject convresponse = JsonConvert.DeserializeObject<Rootobject>(responseBody);

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.ReadLine();
        }
        #endregion
    }

    //JSON Classes
    #region 
    public class Rootobject
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string part_title { get; set; }
        public int real_price { get; set; }
        public string sf_name { get; set; }
        public Photos photos { get; set; }
        public string se_name { get; set; }
        public string part_status { get; set; }
        public string url_part { get; set; }
        public int sup_yearcreate { get; set; }
    }

    public class Photos
    {
        public string path { get; set; }
        public int[] img { get; set; }
    }
    #endregion
    
    public class Advert
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public Advert(string title, string url, string price, string description, string photo = null)
        {
            Title = title;
            Url = url;
            Price = price;
            Description = description;
            Photo = photo;
        }
    }

}
