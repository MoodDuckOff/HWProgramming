using System.ComponentModel.DataAnnotations;

namespace HWP_backend.Models.DTO.Tasks
{
    public class CreateTaskModelDTO
    {
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int AuthorId { get; set; }
    }
}