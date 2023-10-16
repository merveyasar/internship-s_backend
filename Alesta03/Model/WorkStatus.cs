using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alesta03.Model
{
    public class WorkStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Person Person { get; set; }
        public int ?PersonId { get; set; }

        public BackWork BackWork { get; set; }
        public int ?BackWorkId { get; set; }
    }
}
