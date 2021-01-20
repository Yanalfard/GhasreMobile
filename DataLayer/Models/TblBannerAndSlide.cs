using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblBannerAndSlide
    {
        [Key]
        public int BannerAndSlideId { get; set; }
        [Required]
        [StringLength(128)]
        public string Keyword { get; set; }
        [Required]
        [StringLength(4000)]
        public string Value { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActiveTill { get; set; }
    }
}
