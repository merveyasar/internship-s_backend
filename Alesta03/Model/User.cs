using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alesta03.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; }
        public bool IsFirstLogin { get; set; }=true;

        public List<Company> Companies { get; set; }
        public List<Person> People { get; set; }

        
    }
}
