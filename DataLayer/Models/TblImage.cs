using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblImage", Schema = "dbo")]
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
        public int? AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty(nameof(TblAlbum.TblImage))]
        public virtual TblAlbum Album { get; set; }
        [InverseProperty("Image")]
        public virtual ICollection<TblProductImageRel> TblProductImageRel { get; set; }
        [InverseProperty("Image")]
        public virtual ICollection<TblStoreImageRel> TblStoreImageRel { get; set; }
    }
}
