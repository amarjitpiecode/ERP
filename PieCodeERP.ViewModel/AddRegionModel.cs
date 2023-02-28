using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PieCodeERP.ViewModel
{
   
    public class ListRegionModel
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddRegionModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Region Name")]
        public string RegionName { get; set; }
        [Required]
        [DisplayName("Region Code")]
        public string RegionCode { get; set; }
        [Required]

        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
    }
    public class UpdateRegionModel
    {
        public int RegionId { get; set; }
        [Required]
        public string RegionName { get; set; }
        public string RegionCode { get; set; }

    }
    public class DeleteRegionModel
    {
        public int RegionId { get; set; }
    }
}
