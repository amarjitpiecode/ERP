
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace PieCodeERP.ViewModel
{
    public class ListCompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddCompanyModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [DisplayName("Company Code")]
        public string CompanyCode { get; set; }
        [Required]

        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
    }
    public class UpdateCompanyModel
    {
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }

    }
    public class DeleteCompanyModel
    {
        public int CompanyId { get; set; }
    }
}
