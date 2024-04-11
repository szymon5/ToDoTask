using ToDoTask.Interfaces;
using ToDoTask.Models;

namespace ToDoTask.ViewModel
{
    public class EditRemoveViewModel
    {
        private readonly IRepository _repository;

        private int singleTaskID;

        private string day = "";

        private SingleTask singleTask;

        public SingleTask SingleTask => singleTask;

        public EditRemoveViewModel(IRepository repository, int singleTaskID)
        {
            _repository = repository;
            this.singleTaskID = singleTaskID;
        }

        public void GetSingleTaskToUpdate()
        {
            singleTask = _repository.GetTaskByID(singleTaskID);
            day = singleTask.Day.ToString();
        }

        public string EditTask(string title, string description)
        {
            if(title != "" && description != "" && day != "")
            {
                var updated = _repository.UpdateTask(new SingleTask()
                {
                    Id = singleTaskID,
                    Title = title,
                    Description = description,
                    Day = Convert.ToDateTime(day == "" ? singleTask.Day : day)
                });

                if (updated)
                {
                    day = "";
                    MainWindowViewModel.OnPageRefresh();
                    return "Task has been updated";
                }
                else return "Task has not been updated";
            }
            else return "You have to provide all data";
        }

        public void SetTaskDay(string day) => this.day = day;

        public bool RemoveTask()
        {
            var removed = _repository.RemoveTask(singleTaskID);
            if (removed)
            {
                MainWindowViewModel.OnPageRefresh();
                return true;
            }
            else return false;
        }
    }
}
