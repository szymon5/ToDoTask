using ToDoTask.Interfaces;
using ToDoTask.Models;
using ToDoTask.Services;

namespace ToDoTask.Repositories
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context) => this.context = context;

        public bool AddTask(SingleTask singleTask)
        {
            context.SingleTasks.Add(singleTask);
            if (context.SaveChanges() == 1) return true;
            else return false;
        }

        public List<SingleTask> GetTasks(DateTime day) => this.context.SingleTasks.Where(x => x.Day == day).ToList();
    }
}
