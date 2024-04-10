using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoTask.Interfaces;
using ToDoTask.ViewModel;

namespace ToDoTask
{
    /// <summary>
    /// Interaction logic for AddNewTask.xaml
    /// </summary>
    public partial class AddNewTask : Window
    {
        private readonly IRepository _repository;

        AddNewTaskViewModel MemoriesInTheSpecificGroupViewModel
        {
            get => DataContext as AddNewTaskViewModel;
            set => DataContext = this;
        }

        public AddNewTask(IRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            DataContext = new AddNewTaskViewModel(_repository);
        }

        private void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            var added = MemoriesInTheSpecificGroupViewModel.AddNewTask(TaskTitle.Text, TaskDescription.Text);
            if (added) Close();
        }

        private void TaskCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskDay.Text = NewTaskCalendar.SelectedDate.ToString();
            MemoriesInTheSpecificGroupViewModel.SetTaskDay(NewTaskCalendar.SelectedDate.ToString());
        }
    }
}
