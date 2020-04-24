using BeestjeOpJeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Domain.Interface_Repositories
{
    public interface IContactpersonRepository : IRepository<ContactPerson>
    {
        ContactpersonVM TempPerson { get; set; }
    }

    
}
