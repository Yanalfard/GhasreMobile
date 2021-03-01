using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblAd
    {
        [Key]
        public int AdId { get; set; }
        [Required]
        public string Link { get; set; }
        
        public string Image { get; set; }
        public int PositionId { get; set; }
        [StringLength(500)]
        public string Label { get; set; }
    }
}
