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
        [Required(ErrorMessage ="نام فروشگاه را وارد کنید")]
        [StringLength(100,ErrorMessage ="نام فروشگاه معتبر وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage ="شماره تماس فروشگاه را وارد کنید")]
        [StringLength(11,ErrorMessage ="شماره تماس معتبر وارد کنید")]
        public string TellNo { get; set; }
        [Required(ErrorMessage ="موقعیت جغرافیایی فروشگاه را وارد کنید")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "موقعیت جغرافیایی فروشگاه را وارد کنید")]
        public string Lon { get; set; }
        public string MainImage { get; set; }
        public string Body { get; set; }
        [Required(ErrorMessage ="لطفا آدرس فروشگاه را وارد کنید")]
        [StringLength(500,ErrorMessage ="آدرس معتبر وارد کنید")]
        public string Address { get; set; }

        [InverseProperty("Image")]
        public virtual ICollection<TblStoreImageRel> TblStoreImageRel { get; set; }
    }
}
