
using System.ComponentModel.DataAnnotations;

namespace PieCodeERP.ViewModel
{
    public class ListDepartmentModel
    {

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddDepartmentModel
    {
        public int Id { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        [Required]

        public bool IsActive { get; set; }
        [System.ComponentModel.DisplayName("Department Code")]

        public string DepartmentCode { get; set; }

        public DateTime? CreationDate { get; set; }
    }
    public class UpdateDepartmentModel
    {
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }

    }
    public class DeleteDepartmentrModel
    {
        public int DepartmentId { get; set; }
    }
}
