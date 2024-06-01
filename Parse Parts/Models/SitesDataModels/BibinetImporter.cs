using Newtonsoft.Json;
using Parse_Parts.Infrastructure.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Parse_Parts.Models.SitesDataModels
{
    internal class BibinetImporter : ISiteImporter
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<Collection<Advert>> GetData(string OemId)
        {
            var data = new Collection<Advert>();
            try
            {
                HttpContent content = new StringContent("{\"ver_api\":\"v3\",\"ver2\":1,\"kind\":\"profi\",\"parts_per_page\":50,\"city\":38,\"raw_oem_code\":\" 1530170100\"}"); //TODO: Сделать динамический ввод номера
                HttpResponseMessage response = await client.PostAsync("https://bibinet.ru/service/search/v5/parts/", content);
                response.EnsureSuccessStatusCode();
                JsonSerializer jsonSerializer = new JsonSerializer();
                string responseBody = await response.Content.ReadAsStringAsync();
                Rootobject convresponse = JsonConvert.DeserializeObject<Rootobject>(responseBody);
                foreach (var item in convresponse.response.data) 
                {
                    data.Add(new Advert(
                        item.part_title,
                        "https://bibinet.ru/" + item.url_part,
                        item.real_price.ToString(),
                        item.sup_part_comment,
                        "https://bibinet.ru/" + item.photos.path
                        ));
                }
                return data;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }

    //JSON Class
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
        public string sup_part_comment { get; set; }
    }

    public class Photos
    {
        public string path { get; set; }
        public int[] img { get; set; }
    }
    #endregion
}
