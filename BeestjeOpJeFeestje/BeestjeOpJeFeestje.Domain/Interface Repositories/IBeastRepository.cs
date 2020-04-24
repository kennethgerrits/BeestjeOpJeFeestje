using BeestjeOpJeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interface_Repositories
{
    public interface IBeastRepository : IRepository<Beast>
    {
        IEnumerable<BeastVM> TempSelected { get; set; }


        IEnumerable<BeastVM> BeastsAvailable(DateTime date);

        void UpdateBeast(BeastVM beast);

        bool ExcludePinguin { get; set; }
        bool ExcludeDesert { get; set; }
        bool ExcludeSnow { get; set; }
        bool ExcludeFarm { get; set; }
        bool ExcludePolarLion { get; set; }
        bool ExcludeUnavailable { get; set; }
        void SetFiltersToDefault();

    }
}
