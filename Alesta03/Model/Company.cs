using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alesta03.Model
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FDate { get; set; }
        public string TotalStaff { get; set; }
        public string Location { get; set; }
        public string Prof { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string ?Image { get; set; }  

        public User Users { get; set; }
        public int ?UsersId { get; set; }

        public List<ExpReview> Reviews { get; set; }
        public List<ApprovalStatus> ApprovalStatuses { get; set; }
    }
}
