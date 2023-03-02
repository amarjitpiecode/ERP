using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieCodeERP.ViewModel
{
    public class ListClassificationsModel
    {

        public int ClassificationsId { get; set; }
        public string ClassificationsName { get; set; }
        public string ClassificationsCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class AddClassificationsModel
    {
        public int Id { get; set; }

        [Required]
        [System.ComponentModel.DisplayName("Classifications Name")]
        public string ClassificationsName { get; set; }
        [Required]

        public bool IsActive { get; set; }
        [System.ComponentModel.DisplayName("Classifications Code")]

        public string ClassificationsCode { get; set; }

        public DateTime? CreationDate { get; set; }
    }
    public class UpdateClassificationsModel
    {
        public int ClassificationsId { get; set; }
        [Required]
        public string ClassificationsName { get; set; }
        public string ClassificationsCode { get; set; }

    }
    public class DeleteClassificationsrModel
    {
        public int ClassificationsId { get; set; }
    }
}
