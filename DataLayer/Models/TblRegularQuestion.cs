using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class TblRegularQuestion
    {
        [Key]
        public int RegularQuestionId { get; set; }
        [Required(ErrorMessage = "سوال اجباری میباشد")]
        [StringLength(4000, ErrorMessage = "سوال مناسب وارد کنید")]
        public string Question { get; set; }
        [Required(ErrorMessage = "جواب اجباری میباشد")]
        [StringLength(4000, ErrorMessage = "جواب مناسب وارد کنید")]
        public string Answer { get; set; }
    }
}
