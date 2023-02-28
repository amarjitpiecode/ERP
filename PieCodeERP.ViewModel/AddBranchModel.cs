using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PieCodeERP.ViewModel
{

    public class ListBranchModel
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddBranchModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        [Required]
        [DisplayName("Branch Code")]
        public string BranchCode { get; set; }
        [Required]

        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
    }
    public class UpdateBranchModel
    {
        public int BranchId { get; set; }
        [Required]
        public string BranchName { get; set; }
        public string BranchCode { get; set; }

    }
    public class DeleteBranchModel
    {
        public int BranchId { get; set; }
    }
}
