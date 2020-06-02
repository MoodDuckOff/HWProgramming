using System.Collections.Generic;
using System.Linq;
using HWP_backend.Entities;
using HWP_backend.Helpers;

namespace HWP_backend.Services.SolvedTaskServices
{
    public class SolvedTaskService
    {
        private readonly DataContext _context;

        public SolvedTaskService(DataContext context)
        {
            _context = context;


            //    var dummy = _context.Tasks.Include(task => task.SolvedTasks)
            //.Where(x => x.SolvedTasks.Any(solvedTask => solvedTask.UserId == 1));
        }

        public IEnumerable<SolvedTask> GetAllSolvedTasks()
        {
            return _context.SolvedTasks;
        }

        public IEnumerable<SolvedTask> GetSolvedTasksByUserId(int userId)
        {
            var solvedTasks = _context.SolvedTasks.Where(st => st.UserId == userId);
            return solvedTasks;
        }

        public IEnumerable<SolvedTask> GetSolvedTasksByTaskId(int taskId)
        {
            var solvedTasks = _context.SolvedTasks.Where(st => st.TaskId == taskId);
            return solvedTasks;
        }

        public SolvedTask GetSolvedTask(int userId, int taskId)
        {
            var solvedTask = _context.SolvedTasks.FirstOrDefault(task =>
                task.TaskId == taskId &&
                task.UserId == userId);
            return solvedTask;
        }

        public SolvedTask Create(SolvedTask solvedTask, int userId, int taskId)
        {
            if (!_context.Users.Any(user => user.Id == userId))
                throw new AppException("User id null or not found");
            if (!_context.Tasks.Any(task => task.Id == taskId))
                throw new AppException("Task id null or not found");
            solvedTask.TaskId = taskId;
            solvedTask.UserId = userId;
            solvedTask.IsChecked = false;
            solvedTask.Mark = null;
            _context.SolvedTasks.Add(solvedTask);
            _context.SaveChanges();
            return solvedTask;
        }

        //update for student
        public void Update(SolvedTask solvedTaskParam)
        {
            var solvedTask = _context.SolvedTasks.FirstOrDefault(st =>
                st.TaskId == solvedTaskParam.TaskId &&
                st.UserId == solvedTaskParam.UserId);
            if (solvedTask == null)
                throw new AppException("Solved task not found");

            if (_context.Users.Any(user => user.Id == solvedTaskParam.UserId))
                solvedTask.UserId = solvedTaskParam.UserId;

            if (_context.Tasks.Any(task => task.Id == solvedTaskParam.TaskId))
                solvedTask.UserId = solvedTaskParam.UserId;

            if (!string.IsNullOrWhiteSpace(solvedTaskParam.Solution))
                solvedTask.Solution = solvedTaskParam.Solution;

            _context.SolvedTasks.Update(solvedTask);
            _context.SaveChanges();
        }

        public void Delete(int userId, int taskId)
        {
            var solvedTask = _context.SolvedTasks.FirstOrDefault(st =>
                st.UserId == userId &&
                st.TaskId == taskId);
            if (solvedTask == null) return;
            _context.SolvedTasks.Remove(solvedTask);
            _context.SaveChanges();
        }

        //update for teacher
        public void RateSolvedTask(SolvedTask solvedTaskParam)
        {
            var solvedTask = _context.SolvedTasks.FirstOrDefault(st =>
                st.TaskId == solvedTaskParam.TaskId &&
                st.UserId == solvedTaskParam.UserId);
            if (solvedTask == null)
                throw new AppException("Solved task not found");
            if (solvedTaskParam.Mark > 5 || solvedTaskParam.Mark < 1 || solvedTaskParam.Mark == null)
                throw new AppException("Invalid mark");
            if (solvedTaskParam.Mark != null)
                solvedTask.Mark = solvedTaskParam.Mark;
            solvedTask.IsChecked = true;
            _context.SolvedTasks.Update(solvedTask);
            _context.SaveChanges();
        }
    }
}