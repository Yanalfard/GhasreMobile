using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblRole", Schema = "dbo")]
    public partial class TblRole
    {
        public TblRole()
        {
            TblClient = new HashSet<TblClient>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Title { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<TblClient> TblClient { get; set; }
    }
}
