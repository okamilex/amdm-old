using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmdmData;
using AmdmData.Enums;

namespace AmdmLogic
{
    public class Logic
    {
        public static List<Performers> GetListOfPerformers(PerformersSortingTypes performersSortingType, int performersPageNumber, int count)
        {
            return DataAndInfo.GetListOfPerformers(performersSortingType, performersPageNumber, count);
        }
        public static int GetPerformersCount()
        {
            return DataAndInfo.GetPerformersCount();
        }
    }
}
