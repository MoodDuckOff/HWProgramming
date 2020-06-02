using System.Collections.Generic;
using Newtonsoft.Json;

namespace HWP_backend.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

        [JsonIgnore] public IEnumerable<SolvedTask> SolvedTasks { get; set; }
    }
}