using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblVisit", Schema = "dbo")]
    public partial class TblVisit
    {
        [Key]
        public int VisitId { get; set; }
        [StringLength(15)]
        public string Ip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
    }
}
