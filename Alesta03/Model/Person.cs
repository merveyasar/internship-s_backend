using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alesta03.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string ?Image { get; set; }

        public User Users { get; set; }
        public int ?UsersId { get; set; }

        public List<ExpReview> Reviews { get; set; }
        public List<EduStatus> EduStatuses { get; set; }
        public List<WorkStatus> WorkStatuses { get; set; }
        public List<ApprovalStatus> Approvals { get; set; }

    }
}
