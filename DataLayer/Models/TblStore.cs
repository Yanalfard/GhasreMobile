using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblStore
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(200)]
        public string TellNo { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lon { get; set; }
        [Required]
        [StringLength(500)]
        public string MainImage { get; set; }
        public string Body { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
    }
}
