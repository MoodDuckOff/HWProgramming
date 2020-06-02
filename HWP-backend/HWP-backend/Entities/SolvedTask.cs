namespace HWP_backend.Entities
{
    public class SolvedTask
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public string Solution { get; set; }
        public bool IsChecked { get; set; }
        public int? Mark { get; set; }
    }
}