namespace HWP_backend.Models.DTO.SolvedTasks
{
    public class UpdateSolvedTaskDTO
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string Solution { get; set; }
    }
}