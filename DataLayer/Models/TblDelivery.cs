﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblDelivery
    {
        [Key]
        public int DeliveryId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string TellNo { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
    }
}
