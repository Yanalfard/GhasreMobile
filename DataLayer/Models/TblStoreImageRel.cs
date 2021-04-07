using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class TblStoreImageRel
    {
        [Key]
        public int StoreImageRel { get; set; }
        public int StoreId { get; set; }
        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        [InverseProperty(nameof(TblImage.TblStoreImageRel))]
        public virtual TblImage Image { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblStoreImageRel))]
        public virtual TblStore Product { get; set; }
    }
}
