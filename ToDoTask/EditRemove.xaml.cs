using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoTask.Interfaces;
using ToDoTask.Models;
using ToDoTask.ViewModel;

namespace ToDoTask
{
    /// <summary>
    /// Interaction logic for EditRemove.xaml
    /// </summary>
    public partial class EditRemove : Window
    {
        private readonly IRepository _repository;

        EditRemoveViewModel EditRemoveViewModel
        {
            get => DataContext as EditRemoveViewModel;
            set => DataContext = this;
        }

        public EditRemove(IRepository repository, int singleTaskID)
        {
            InitializeComponent();
            _repository = repository;

            DataContext = new EditRemoveViewModel(repository, singleTaskID);

            EditRemoveViewModel.GetSingleTaskToUpdate();

            TaskTitle.Text = EditRemoveViewModel.SingleTask.Title;
            TaskDescription.Text = EditRemoveViewModel.SingleTask.Description;
            TaskDay.Text = EditRemoveViewModel.SingleTask.Day.ToString();
        }

        private void NewTaskCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskDay.Text = NewTaskCalendar.SelectedDate.ToString();
            EditRemoveViewModel.SetTaskDay(NewTaskCalendar.SelectedDate.ToString());
        }

        private void EditTask_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show(EditRemoveViewModel.EditTask(TaskTitle.Text, TaskDescription.Text));

        private void Cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
