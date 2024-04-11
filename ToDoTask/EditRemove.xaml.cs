using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToDoTask.Interfaces;
using ToDoTask.ViewModel;

namespace ToDoTask
{
    public partial class EditRemove : Window
    {
        EditRemoveViewModel EditRemoveViewModel
        {
            get => DataContext as EditRemoveViewModel;
            set => DataContext = this;
        }

        public EditRemove(IRepository repository, int singleTaskID)
        {
            InitializeComponent();

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

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var decision = MessageBox.Show("Are you sure you want to remove this task?","?", MessageBoxButton.YesNo);
            if(decision == MessageBoxResult.Yes)
            {
                var remove = EditRemoveViewModel.RemoveTask();
                if (remove)
                {
                    MessageBox.Show("The task has been removed");
                    Close();
                }
                else MessageBox.Show("The task has not been removed");
            }
        }
    }
}
