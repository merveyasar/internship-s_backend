using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alesta03.Model
{
    public class ApprovalStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Status { get; set; } = String.Empty;

        //r--> red yedi
        //o--> onaylandı
        //b--> bekliyor
        public int ?BackWorkId { get; set; }
        public BackWork BackWork { get; set; }
                
        public int ?CompanyId { get; set; }
        public Company Company { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
