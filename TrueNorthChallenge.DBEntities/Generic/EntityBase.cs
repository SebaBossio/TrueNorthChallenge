using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNorthChallenge.DBEntities.Generic
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime CTS { get; set; } //CreatedTimeStamp
        [Column(TypeName = "VARCHAR(50)")]
        public string CreatedById { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime? MTS { get; set; } //ModifiedTimeStamp
        [Column(TypeName = "VARCHAR(50)")]
        public string ModifiedById { get; set; }
    }
}
