using System.ComponentModel.DataAnnotations;
namespace PiecodeERP.Data
{
    public class RegionMaster:BaseEntity
    {
        [StringLength(256)]
        [Required]
        public string? RegionName { get; set; }

    }
}
