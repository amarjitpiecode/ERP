
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace PieCodeERP.ViewModel
{
    public class ListCostCenterModel
    {
        public int CostCenterId { get; set; }
        public string CCName { get; set; }
        public string CCCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddCostCenterModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("CostCenter Name")]
        public string CostCenterName { get; set; }
        [Required]
        [DisplayName("CostCenter Code")]
        public string CostCenterCode { get; set; }
        [Required]

        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
    }
    public class UpdateCostCenterModel
    {
        public int CostCenterId { get; set; }
        [Required]
        public string CostCenterName { get; set; }
        public string CostCenterCode { get; set; }

    }
    public class DeleteCostCenterModel
    {
        public int CostCenterId { get; set; }
    }
}
