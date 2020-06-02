using System.ComponentModel.DataAnnotations;

namespace HWP_backend.Models.DTO.SolvedTasks
{
    public class CreateSolvedTaskDTO
    {
        [Required] public int UserId { get; set; }
        [Required] public int TaskId { get; set; }
        [Required] public string Solution { get; set; }
    }
}