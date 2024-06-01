using Parse_Parts.Infrastructure.Interfaces;
using Parse_Parts.Models.SitesDataModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Parse_Parts.Models
{
    internal class ImportHub
    {
        private static ImportHub _instance;

        ObservableCollection<ISiteImporter> siteImporters;

        private ImportHub()
        {
            siteImporters = [new AvitoImporter()];
        }

        public static ImportHub getInstance()
        {
            if (_instance == null)
                _instance = new ImportHub();
            return _instance;
        }

        public async Task<Collection<Advert>> getAdverts(string searchParam)
        {

            if (searchParam != null)
            {
                Collection<Advert> adverts = null;
                foreach (ISiteImporter importer in siteImporters)
                {
                    var data = await importer.GetData(searchParam);
                    adverts = new Collection<Advert>(adverts
                        .Concat(data)
                        .ToList());
                }

                return adverts;
            }
            return null;

        }
    }
}
