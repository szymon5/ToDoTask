﻿using System.Windows;
using System.Windows.Controls;
using ToDoTask.Interfaces;
using ToDoTask.Models;
using ToDoTask.ViewModel;

namespace ToDoTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository _repository;

        MainWindowViewModel MainWindowViewModel
        {
            get => DataContext as MainWindowViewModel;
            set => DataContext = this;
        }

        public MainWindow(IRepository repository)
        {
            InitializeComponent();
            _repository = repository;

            DataContext = new MainWindowViewModel(repository);

            MainWindowViewModel.GetAllTasks();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(string.Format("{0:yyyy-MM-dd}", TaskCalendar.SelectedDate));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new AddNewTask(_repository);
            newForm.Show();
        }

        private void TaskList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (TaskList.SelectedItem != null)
            {
                var selectedUser = TaskList.SelectedItem as SingleTask;
                var newForm = new EditRemove(_repository, selectedUser.Id);
                newForm.Show();
            }
        }
    }
}