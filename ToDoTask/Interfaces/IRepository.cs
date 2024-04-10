using ToDoTask.Models;

namespace ToDoTask.Interfaces
{
    public interface IRepository
    {
        public bool AddTask(SingleTask singleTask);
        public List<SingleTask> GetTasks(DateTime day);
    }
}
