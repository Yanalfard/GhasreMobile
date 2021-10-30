using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("TblStore", Schema = "dbo")]
    public partial class TblStore
    {
        [Key]
        public int StoreId { get; set; }
        [Required(ErrorMessage ="نام فروشگاه را وارد کنید")]
        [StringLength(100,ErrorMessage ="نام فروشگاه معتبر وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage ="شماره تماس فروشگاه را وارد کنید")]
        [StringLength(11,ErrorMessage ="شماره تماس معتبر وارد کنید")]
        public string TellNo { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Body { get; set; }
        [Required(ErrorMessage ="لطفا آدرس فروشگاه را وارد کنید")]
        [StringLength(500,ErrorMessage ="آدرس معتبر وارد کنید")]
        public string Address { get; set; }

        [InverseProperty("Store")]
        public virtual ICollection<TblStoreImageRel> TblStoreImageRel { get; set; }
    }
}
