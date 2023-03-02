using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PieCodeERP.ViewModel
{
    public class ListEmployeeModel
    {

        public int EmployeeId { get; set; }
        
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeePhone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddEmployeeModel
    {
        public int EmployeeId { get; set; }
        

        [Required]
        [System.ComponentModel.DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [Required]
        [System.ComponentModel.DisplayName("Employee Address")]
        public string EmployeeAddress { get; set; }
        [Required]
        [System.ComponentModel.DisplayName("Employee Phone")]
        public string EmployeePhone { get; set; }
        [Required]


        public bool IsActive { get; set; }
        [System.ComponentModel.DisplayName("Employeet Code")]

       public int CompanyId { get; set; } 

        public DateTime? CreationDate { get; set; }
    }
    public class UpdateEmployeeModel
    {
        public int EmployeeId { get; set; }
        
        [Required]
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeePhone { get; set; }

    }
    public class DeleteEmployeeModel
    {
        public int EmployeeId { get; set; }
    }
}
