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
            siteImporters = [
                new AvitoImporter(),
                new BibinetImporter()
            ];
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
                var adverts = new Collection<Advert>();
                foreach (ISiteImporter importer in siteImporters)
                {
                    var data = await importer.GetData(searchParam);
                    foreach (var item in data)
                    {
                        item.Photo ??= "/Data/Images/placeholder.png";
                        adverts.Add(item);
                    }
                }

                return adverts;
            }
            return null;

        }
    }
}
