using Parse_Parts.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace Parse_Parts.Models
{
    internal class ImportHub
    {
        private static ImportHub _instance;

        ObservableCollection<ISiteImporter> siteImporters;

        private ImportHub()
        {

        }

        public static ImportHub getInstance()
        {
            if (_instance == null)
                _instance = new ImportHub();
            return _instance;
        }

        public Collection<Advert> getAdverts(string searchParam)
        {

            if (searchParam != null)
            {
                Collection<Advert> adverts = null;
                foreach (ISiteImporter importer in siteImporters)
                {
                    adverts = new Collection<Advert>(adverts
                        .Concat(importer.GetData(searchParam).Result)
                        .ToList());
                }

                return adverts;
            }
            return null;

        }
    }
}
