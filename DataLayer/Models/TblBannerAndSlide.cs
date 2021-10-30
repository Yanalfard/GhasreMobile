using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblBannerAndSlide", Schema = "dbo")]
    public partial class TblBannerAndSlide
    {
        [Key]
        public int BannerAndSlideId { get; set; }
        [Required(ErrorMessage = "نام اسلایدر الزامی میباشد")]
        [StringLength(128, ErrorMessage = "نام اسلایدر مناسب وارد کنید")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "لینک اسلایدر الزامی میباشد")]
        [StringLength(4000, ErrorMessage = "لینک اسلایدر مناسب وارد کنید")]
        public string Link { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActiveTill { get; set; }
    }
}
