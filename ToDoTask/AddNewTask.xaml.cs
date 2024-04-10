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
    public partial class AddNewTask : Window
    {
        private readonly IRepository _repository;

        AddNewTaskViewModel AddNewTaskViewModel
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

        private void AddNewTask_Click(object sender, RoutedEventArgs e) =>
            System.Windows.MessageBox.Show(AddNewTaskViewModel.AddNewTask(TaskTitle.Text, TaskDescription.Text));

        private void TaskCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskDay.Text = NewTaskCalendar.SelectedDate.ToString();
            AddNewTaskViewModel.SetTaskDay(NewTaskCalendar.SelectedDate.ToString());
        }
    }
}
