
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PiecodeERP.Data
{
    [Table("Department")]
    public class Department : BaseEntity
    {
       
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
