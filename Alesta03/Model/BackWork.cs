using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alesta03.Model
{
    public class BackWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeID { get; set; }  
        public string AppLetter { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<ApprovalStatus> ApprovalStatuses { get; set; }
        public List<WorkStatus> WorkStatuses { get; set; }



    }
}
