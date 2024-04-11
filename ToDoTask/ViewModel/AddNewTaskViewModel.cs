using ToDoTask.Interfaces;

namespace ToDoTask.ViewModel
{
    public class AddNewTaskViewModel
    {
        private IRepository _repository;
        private string day = "";

        public AddNewTaskViewModel(IRepository repository)
        {
            _repository = repository;
        }

        public string AddNewTask(string title, string description)
        {
            if (title != "" && description != "" && day != "")
            {
                var added = _repository.AddTask(new Models.SingleTask()
                {
                    Title = title,
                    Description = description,
                    Day = Convert.ToDateTime(day)
                });

                if (added)
                {
                    day = "";
                    MainWindowViewModel.OnPageRefresh();
                    return "New task has been added";
                }
                else return "Something went wrong. New task has not been added";
            }
            else return "You have to provide all data";
        }

        public void SetTaskDay(string day) => this.day = day;
    }
}
