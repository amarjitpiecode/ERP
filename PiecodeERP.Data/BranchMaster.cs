using System.ComponentModel.DataAnnotations.Schema;

namespace PiecodeERP.Data
{
    [Table("Branches")]
    public class BranchMaster : BaseEntity
    {

        public string BranchName { get; set; }
        public string BranchCode { get; set; }

    }
}
