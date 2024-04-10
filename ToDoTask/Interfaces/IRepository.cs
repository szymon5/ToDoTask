using ToDoTask.Models;

namespace ToDoTask.Interfaces
{
    public interface IRepository
    {
        public bool AddTask(SingleTask singleTask);
        public SingleTask GetTaskByID(int id);
        public List<SingleTask> GetAllTasks();
        public bool EditTask(SingleTask singleTask);
        public List<SingleTask> GetTasks(DateTime day);
    }
}
