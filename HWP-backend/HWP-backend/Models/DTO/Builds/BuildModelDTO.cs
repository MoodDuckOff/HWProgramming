namespace HWP_backend.Models.DTO.Builds
{
    public class BuildModelDTO
    {
        public string Code { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public bool IsSuccess { get; set; }
    }
}