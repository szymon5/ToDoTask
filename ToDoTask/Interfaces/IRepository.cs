using ToDoTask.Models;

namespace ToDoTask.Interfaces
{
    public interface IRepository
    {
        public bool AddTask(SingleTask singleTask);
        public SingleTask GetTaskByID(int id);
        public List<SingleTask> GetAllTasks();
        public bool UpdateTask(SingleTask singleTask);
        public bool RemoveTask(int id);
        public List<SingleTask> GetTasksByDay(DateTime day);
    }
}
