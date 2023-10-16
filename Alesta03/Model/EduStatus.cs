using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alesta03.Model
{
    public class EduStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Person Persons{ get; set; }
        public int ?PersonId { get; set; }

        public BackEdu BackEdu { get; set; }
        public int ?BackEduId { get; set; }
        }
}
