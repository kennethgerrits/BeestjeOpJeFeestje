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
    public class AccessoryRepository : Repository<Accessory>, IAccessoryRepository
    {

        public AccessoryRepository(BeesteOpJeFeestjeEntities context) : base(context)
        {

        }

        public void UpdateAccessory(AccessoryVM acc)
        {
            Context.Set<Accessory>().AddOrUpdate(acc.Accessory);
        }

        public void RemoveRange(IEnumerable<AccessoryVM> accessories)
        {
            foreach (var accessory in accessories)
            {
                accessory.Booking.Clear();
            }
            Context.Set<Accessory>().RemoveRange(accessories.Select(a => a.Accessory));
        }

    }
}
