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

        public bool UpdateTask(SingleTask singleTask)
        {
            var currentSingleTask = context.SingleTasks.FirstOrDefault(x => x.Id == singleTask.Id);

            if(currentSingleTask != null)
            {
                currentSingleTask.Title = singleTask.Title;
                currentSingleTask.Description = singleTask.Description;
                currentSingleTask.Day = singleTask.Day;

                if (context.SaveChanges() == 1) return true;
                else return false;
            }
            else return false;
        }

        public List<SingleTask> GetAllTasks() => context.SingleTasks.ToList();

        public SingleTask GetTaskByID(int id) => context.SingleTasks.FirstOrDefault(x => x.Id == id);

        public bool RemoveTask(int id)
        {
            var task = context.SingleTasks.FirstOrDefault(x => x.Id == id);

            if(task != null)
            {
                context.SingleTasks.Remove(task);
                if (context.SaveChanges() == 1) return true;
                else return false;
            }

            else return false;
        }

        public List<SingleTask> GetTasks(DateTime day) => context.SingleTasks.Where(x => x.Day == day).ToList();
    }
}
