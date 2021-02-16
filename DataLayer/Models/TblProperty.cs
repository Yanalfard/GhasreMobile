using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblProperty
    {
        public TblProperty()
        {
            TblProductPropertyRel = new HashSet<TblProductPropertyRel>();
        }

        [Key]
        public int PropertyId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty("Property")]
        public virtual ICollection<TblProductPropertyRel> TblProductPropertyRel { get; set; }
    }
}
