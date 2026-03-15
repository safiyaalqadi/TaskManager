namespace TaskManagerProj.Entitys
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public string statuses { get; set; }

        public TaskEntity() { 
          UserEntity = new UserEntity();
        }

        public UserEntity UserEntity { get; set; }



    }
}
