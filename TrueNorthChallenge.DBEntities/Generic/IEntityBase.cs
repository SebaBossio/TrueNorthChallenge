using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNorthChallenge.DBEntities.Generic
{
    public interface IEntityBase
    {
        int Id { get; set; }
        DateTime CTS { get; set; }
        DateTime? MTS { get; set; }
    }
}
