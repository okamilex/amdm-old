using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmdmData
{
    public class DataAndInfo
    {
        public static List<Performers> GetListOfPerformers(PerformersSortingTypes performersSortingType, int performersPageNumber, int count)
        {
            var db = new AmdmContext();
            var performersContext = db.Performers.AsQueryable();
            var performersResolt = performersContext;
            performersContext.OrderBy(x => x.Id);
            switch (performersSortingType)
            {
                case PerformersSortingTypes.ById:
                    performersResolt = performersContext.OrderBy(x => x.Id)
                        .Skip((performersPageNumber - 1) * count)
                        .Take(count);
                    break;
                case PerformersSortingTypes.ByIdBack:
                    performersResolt = performersContext.OrderBy(x => -x.Id)
                        .Skip((performersPageNumber - 1) * count)
                        .Take(count);
                    break;
                case PerformersSortingTypes.ByName:
                    performersResolt = performersContext.OrderBy(x => x.Name)
                        .Skip((performersPageNumber - 1) * count)
                        .Take(count);
                    break;
                case PerformersSortingTypes.ByNameBack:
                    performersResolt = performersContext.OrderBy(x => x.Name)
                        .Skip((performersPageNumber - 1) * count)
                        .Take(count);
                    break;
                case PerformersSortingTypes.BySongCount:
                    performersResolt = performersContext.OrderBy(x => x.Songs.Count)
                        .Skip((performersPageNumber - 1) * count)
                        .Take(count);
                    break;
                case PerformersSortingTypes.BySongCountBack:
                    performersResolt = performersContext.OrderBy(x => -x.Songs.Count)
                        .Skip((performersPageNumber - 1) * count)
                        .Take(10);
                    break;
                default:
                    break;
            }
            return performersResolt.ToList();            
        }
        public static int GetPerformersCount()
        {
            return new AmdmContext().Performers.Count();
        }
    }
}
