using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblConfig
    {
        [Key]
        [StringLength(128)]
        public string Key { get; set; }
        [StringLength(500)]
        public string Value { get; set; }
    }
}
