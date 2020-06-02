namespace HWP_backend.Models.DTO.Tasks
{
    public class TaskModelDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}