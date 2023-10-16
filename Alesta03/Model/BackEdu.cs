using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alesta03.Model
{
    public class BackEdu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string SchoolName { get; set; }
        public string DepartmentName { get; set; }
        public string SchoolType { get; set; }
        public bool EduStatus { get; set; }
        public float Avg { get; set; }

        public List<EduStatus> EduStatuses { get; set; }
       
    }
}
