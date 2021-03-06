using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblPostOption
    {
        [Key]
        public int PostOptionId { get; set; }
        [Required(ErrorMessage ="نحوه ارسال اجباری میباشد")]
        [MaxLength(100,ErrorMessage ="نحوه ارسال مناسب وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage ="هزینه ارسال اجباری میباشد")]
        public int? Price { get; set; }
    }
}
