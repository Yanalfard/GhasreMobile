using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    [Table("TblContactUs", Schema = "dbo")]
    public class TblContactUs
    {
        [Key]
        public int ContactUsId { get; set; }
        [Required(ErrorMessage = "نام را وارد کنید")]
        [StringLength(50, ErrorMessage = "کارکتر کمتری وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "شماره تماس را وارد کنید")]
        [StringLength(15, ErrorMessage = "شماره تماس معتبر وارد کنید")]
        public string TellNo { get; set; }
        [Required(ErrorMessage = "پیغام را وارد کنید")]
        [StringLength(500, ErrorMessage = "کارکتر های کمتری وارد کنید")]
        public string Message { get; set; }
    }
}
