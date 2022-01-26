using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zedcrest.Domain.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
        DateTime? DateCreated { get; set; }
    }
}
