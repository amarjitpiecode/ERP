
using System.ComponentModel.DataAnnotations.Schema;


namespace PiecodeERP.Data
{
    [Table("Classificationss")]
    public class Classifications :BaseEntity
    {
      public string?  ClassificationName { get; set; }
      public string? ClassificationCode { get; set; }
    }
}
