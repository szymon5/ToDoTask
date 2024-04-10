using System.Windows;
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

        public bool AddNewTask(string title, string description)
        {
            if (title != "" && description != "" && day != "")
            {
                var added = _repository.AddTask(new Models.SingleTask()
                {
                    Title = title,
                    Description = description,
                    Day = Convert.ToDateTime(day)
                });

                day = "";

                if (added)
                {
                    MessageBox.Show("New task has been added");
                    return true;
                }
                else
                {
                    MessageBox.Show("Something went wrong. New task has not been added");
                    return false;
                }
            }
            else
            {
                day = "";
                MessageBox.Show("You have to provide all data");
                return false;
            }
        }

        public void SetTaskDay(string day) => this.day = day;
    }
}
