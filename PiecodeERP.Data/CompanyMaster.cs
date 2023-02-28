
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiecodeERP.Data
{
    [Table("Companies")]
    public class CompanyMaster:BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
    }
}
