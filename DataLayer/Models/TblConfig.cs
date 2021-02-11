using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public partial class TblConfig
    {
        [Key]
        [StringLength(128)]
        public string Key { get; set; }
        [StringLength(500)]
        public string Value { get; set; }
    }
}
