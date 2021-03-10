using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblDelivery
    {
        [Key]
        public int DeliveryId { get; set; }
        [Required(ErrorMessage ="نام را وارد کنید")]
        [StringLength(150,ErrorMessage ="نام مناسب وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage ="شماره تماس را وارد کنید")]
        [StringLength(11,ErrorMessage ="شماره تماس مناسب وارد کنید")]
        [MinLength(11, ErrorMessage = "شماره تماس مناسب وارد کنید")]
        public string TellNo { get; set; }
        [Required(ErrorMessage ="لطفا ادرس را وارد کنید")]
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(500)]
        public string Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public bool IsAccepted { get; set; }
    }
}
