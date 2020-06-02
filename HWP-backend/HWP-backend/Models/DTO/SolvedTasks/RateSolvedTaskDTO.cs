using System.ComponentModel.DataAnnotations;

namespace HWP_backend.Models.DTO.SolvedTasks
{
    public class RateSolvedTaskDTO
    {
        [Required] public int UserId { get; set; }
        [Required] public int TaskId { get; set; }
        [Required] public int? Mark { get; set; }
    }
}