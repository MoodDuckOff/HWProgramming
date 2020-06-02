using System.Collections.Generic;
using System.Linq;
using HWP_backend.Entities;
using HWP_backend.Helpers;

namespace HWP_backend.Services.TaskServices
{
    public class TaskService
    {
        private readonly DataContext _context;

        public TaskService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetAll()
        {
            return _context.Tasks;
        }

        public Task GetById(int id)
        {
            return _context.Tasks.FirstOrDefault(task => task.Id == id);
        }

        public Task Create(Task task, int authorId)
        {
            if (!_context.Users.Any(user => user.Id == authorId))
                throw new AppException("Author id null or not found");
            task.AuthorId = authorId;
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public void Update(Task taskParam)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskParam.Id);
            if (task == null)
                throw new AppException("Task not found");

            if (_context.Users.Any(user => user.Id == taskParam.AuthorId))
                task.AuthorId = taskParam.AuthorId;
            if (!string.IsNullOrWhiteSpace(taskParam.Description))
                task.Description = taskParam.Description;
            if (!string.IsNullOrWhiteSpace(taskParam.Title))
                task.Title = taskParam.Title;
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return;
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}