﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblImage
    {
        public TblImage()
        {
            TblProductImageRel = new HashSet<TblProductImageRel>();
        }

        [Key]
        public int ImageId { get; set; }
        [Required]
        public string Image { get; set; }

        [InverseProperty("Image")]
        public virtual ICollection<TblProductImageRel> TblProductImageRel { get; set; }
    }
}