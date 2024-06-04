using AngleSharp;
using AngleSharp.Dom;
using Parse_Parts.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parse_Parts.Models.SitesDataModels
{
    internal class AvitoImporter : ISiteImporter
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<Collection<Advert>> GetData(string OemId)
        {
            var data = new Collection<Advert>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(
                    "https://www.avito.ru/krasnoyarsk?cd=1&q=" + OemId);
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
                    var title = advert.QuerySelectorAll("h3").First(tag =>
                            tag.GetAttribute("itemprop") == "name").TextContent;
                    var url = "https://www.avito.ru/" + advert.QuerySelector("a").GetAttribute("href");
                    var price = advert.QuerySelector("strong").TextContent;   
                    var description = advert.QuerySelectorAll("p").First(tag => tag.ClassName ==
                            "styles-module-root-YczkZ " +
                            "styles-module-size_s-AGMw8 " +
                            "styles-module-size_s_compensated-UgWYW " +
                            "styles-module-size_s-_z7mI " +
                            "styles-module-ellipsis-a2Uq1 " +
                            "stylesMarningNormal-module-root-S7NIr " +
                            "stylesMarningNormal-module-paragraph-s-Yhr2e " +
                            "styles-module-noAccent-LowZ8 " +
                            "styles-module-root_bottom-G4JNz " +
                            "styles-module-margin-bottom_6-_aVZm").TextContent;
                    
                    string photo = null;
                    if(advert.QuerySelector("li").GetAttribute("data-marker") != null)
                        photo=advert.QuerySelectorAll("li").First(tag =>
                            tag.GetAttribute("data-marker").Contains("slider-image/image"))
                               .GetAttribute("data-marker").Replace("slider-image/image-", "");

                    Advert adv = new Advert(
                        title,
                        url,
                        price,
                        description,
                        photo
                        );
                        
                    data.Add(adv);

                }
                return data;


            }
            catch(HttpRequestException e) 
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}
