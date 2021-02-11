using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public partial class TblAd
    {
        [Key]
        public int AdId { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Image { get; set; }
        public int PositionId { get; set; }
        [StringLength(500)]
        public string Label { get; set; }
    }
}
