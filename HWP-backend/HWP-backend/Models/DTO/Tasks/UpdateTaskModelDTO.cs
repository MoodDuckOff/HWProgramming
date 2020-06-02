namespace HWP_backend.Models.DTO.Tasks
{
    public class UpdateTaskModelDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}