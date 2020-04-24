using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using BeestjeOpJeFeestje.Domain.Models;

namespace BeestjeOpJeFeestje.Domain.Repositories
{
    public class BeastRepository : Repository<Beast>, IBeastRepository
    {

        public BeastRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {
        }

        public IEnumerable<BeastVM> TempSelected { get; set; }


        public void UpdateBeast(BeastVM beast)
        {
            Context.Set<Beast>().AddOrUpdate(beast.Beast);
        }

        public IEnumerable<BeastVM> BeastsAvailable(DateTime date)
        {
            var list = GetAll().ToList();
            if (ExcludeUnavailable)
            {
                var unavailable = Context.GetUnavailableBeasts(date.Date).ToList();


                foreach (var item in unavailable)
                {
                    list.Remove(item);
                }
                
            }
            if (ExcludePinguin == true)
            {
                var pin = list.Where(beast => beast.Name == "Pinguin").SingleOrDefault();
                if (pin != null)
                {
                    list.Remove(pin);
                }

            }
            if (ExcludePolarLion == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if(list[i].Name == "Leeuw" || list[i].Name == "Ijsbeer")
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if(ExcludeSnow == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if (list[i].Type == "Sneeuw")
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if(ExcludeDesert == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if (list[i].Type == "Woestijn")
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if(ExcludeFarm == true)
            {
                for (int i = list.Count-1; i >= 0; i--)
                {
                    if (list[i].Type == "Boerderij")
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            return list.Select(b => new BeastVM(b));

        }

        public bool ExcludePinguin { get; set; }
        public bool ExcludeDesert { get; set; }
        public bool ExcludeSnow { get; set; }
        public bool ExcludeFarm { get; set; }
        public bool ExcludePolarLion { get; set; }

        public bool ExcludeUnavailable { get; set; } = false;

        public void SetFiltersToDefault()
        {
            ExcludePinguin = false;
            ExcludeDesert = false;
            ExcludeSnow = false;
            ExcludeFarm = false;
            ExcludePolarLion = false;
        }
    }
}
