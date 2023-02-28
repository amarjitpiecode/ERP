
using System.ComponentModel.DataAnnotations.Schema;


namespace PiecodeERP.Data
{
    [Table("CostCenters")]
    public class CostCenter : BaseEntity
    {
        public string CostCenterName { get; set; }
        public string CostCenterCode { get; set; }
    }
}

