using System.Collections.Generic;
using HWP_backend.Models.DTO.Tasks;

namespace HWP_backend.Models.DTO.Users
{
    public class UserModelDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<TaskModelDTO> Tasks { get; set; }
    }
}