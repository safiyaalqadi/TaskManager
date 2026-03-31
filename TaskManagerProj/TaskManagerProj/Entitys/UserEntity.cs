
using System.ComponentModel.DataAnnotations;

namespace TaskManagerProj.Entitys
{
    public class UserEntity
    {

       public int id {  get; set; }

        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string phoneNumber { get; set; } = string.Empty;
        public string emailConfirmed { get; set; }
        public string role { get; set; }

        public UserEntity() {
            Tasks = new HashSet<TaskEntity>();
        }

        public ICollection<TaskEntity> Tasks { get; set; }



            
          
    }
}
