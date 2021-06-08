using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.DBEntities.Generic;

namespace TrueNorthChallenge.DBEntities
{
    [Table("Tag", Schema = "dbo")]
    public class Tag : EntityBase
    {
        [Column(TypeName = "VARCHAR(500)")]
        public string Name { get; set; }

        public virtual ICollection<Posts_Tags> Posts_Tags { get; set; }
    }
}
