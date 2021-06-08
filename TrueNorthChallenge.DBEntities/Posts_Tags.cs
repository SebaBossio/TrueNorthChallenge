using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.DBEntities.Generic;

namespace TrueNorthChallenge.DBEntities
{
    [Table("Posts_Tags", Schema = "dbo")]
    public class Posts_Tags : EntityBase
    {
        [Column(TypeName = "INT")]
        public int PostId { get; set; }
        [Column(TypeName = "INT")]
        public int TagId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
