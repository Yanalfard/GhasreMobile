using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblAd", Schema = "dbo")]
    public partial class TblAd
    {
        [Key]
        public int AdId { get; set; }
        [Required(ErrorMessage ="لینک اجباری میباشد")]
        public string Link { get; set; }
        public string Image { get; set; }
        public int PositionId { get; set; }
        [Required(ErrorMessage ="نام اجباری میباشد")]
        [StringLength(500)]
        public string Label { get; set; }
    }
}
