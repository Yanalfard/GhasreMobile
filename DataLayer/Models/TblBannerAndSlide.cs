using System;
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
        public string Name { get; set; }
        [Required]
        [StringLength(4000)]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(4000)]
        public string Link { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActiveTill { get; set; }
    }
}
