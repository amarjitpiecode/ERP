using PiecodeERP.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PieCodeErp.Models
{
    [Table("Regions")]
    public class Region:BaseEntity
    {

        public string RegionName{ get; set; }
        public string RegionCode { get; set; }

    }
}
