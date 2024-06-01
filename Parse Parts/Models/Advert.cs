using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_Parts.Models
{
    internal class Advert
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Photo {  get; set; }

        public Advert(string title, string url, string price, string description, string photo=null)
        {
            Title = title;
            Url = url;
            Price = price;
            Description = description;
            Photo = photo;
        }
    }
}
