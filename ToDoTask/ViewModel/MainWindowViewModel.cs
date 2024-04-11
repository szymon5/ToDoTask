using System.Collections.ObjectModel;
using ToDoTask.Interfaces;
using ToDoTask.Models;

namespace ToDoTask.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly IRepository _repository;

        private static event EventHandler RefreshPage;

        public ObservableCollection<SingleTask> SingleTasks { get; set; }

        public MainWindowViewModel(IRepository repository)
        {
            _repository = repository;
            SingleTasks = new ObservableCollection<SingleTask>();

            RefreshPage += MainWindowViewModel_RefreshPage;
        }

        private void MainWindowViewModel_RefreshPage(object? sender, EventArgs e)
        {
            SingleTasks.Clear();
            GetAllTasks();
        }

        public static void OnPageRefresh() => RefreshPage?.Invoke(typeof(MainWindowViewModel), new EventArgs());

        public void GetAllTasks()
        {
            SingleTasks.Clear();

            var tasks = _repository.GetAllTasks();

            if(tasks != null)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    SingleTasks.Add(tasks[i]);
                }
            }
        }

        public void GetTasksByDay(DateTime day)
        {
            SingleTasks.Clear();

            var tasks = _repository.GetTasksByDay(day);

            for (int i = 0; i < tasks.Count; i++)
            {
                SingleTasks.Add(tasks[i]);
            }
        }

        public void ClearList() => SingleTasks.Clear();
    }
}
