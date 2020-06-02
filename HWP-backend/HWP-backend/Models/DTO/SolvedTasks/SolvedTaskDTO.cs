namespace HWP_backend.Models.DTO.SolvedTasks
{
    public class SolvedTaskDTO
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string Solution { get; set; }
        public bool IsChecked { get; set; }
        public int? Mark { get; set; }
    }
}