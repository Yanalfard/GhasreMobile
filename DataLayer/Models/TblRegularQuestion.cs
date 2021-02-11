using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public partial class TblRegularQuestion
    {
        [Key]
        public int RegularQuestionId { get; set; }
        [Required]
        [StringLength(4000)]
        public string Question { get; set; }
        [Required]
        [StringLength(4000)]
        public string Answer { get; set; }
    }
}
