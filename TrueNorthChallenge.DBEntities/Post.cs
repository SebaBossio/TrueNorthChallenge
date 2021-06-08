using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.DBEntities.Generic;

namespace TrueNorthChallenge.DBEntities
{
    [Table("Posts", Schema = "dbo")]
    public class Post : EntityBase
    {
        [Column(TypeName = "VARCHAR(500)")]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Content { get; set; }

        public virtual ICollection<Posts_Tags> Posts_Tags { get; set; }
    }
}
