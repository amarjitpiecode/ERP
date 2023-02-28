
using System.ComponentModel.DataAnnotations.Schema;


namespace PiecodeERP.Data
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeePhone { get; set; }
    }
}
