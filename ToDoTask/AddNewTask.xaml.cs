using System.Windows;
using System.Windows.Controls;
using ToDoTask.Interfaces;
using ToDoTask.ViewModel;

namespace ToDoTask
{
    public partial class AddNewTask : Window
    {
        AddNewTaskViewModel AddNewTaskViewModel
        {
            get => DataContext as AddNewTaskViewModel;
            set => DataContext = this;
        }

        public AddNewTask(IRepository repository)
        {
            InitializeComponent();
            DataContext = new AddNewTaskViewModel(repository);
        }

        private void AddNewTask_Click(object sender, RoutedEventArgs e) =>
            System.Windows.MessageBox.Show(AddNewTaskViewModel.AddNewTask(TaskTitle.Text, TaskDescription.Text));

        private void TaskCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskDay.Text = NewTaskCalendar.SelectedDate.ToString();
            AddNewTaskViewModel.SetTaskDay(NewTaskCalendar.SelectedDate.ToString());
        }
    }
}
