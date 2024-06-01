using Parse_Parts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_Parts.Infrastructure.Interfaces
{
    internal interface ISiteImporter
    {
        Collection<Advert> GetData(string OemId);
    }
}
