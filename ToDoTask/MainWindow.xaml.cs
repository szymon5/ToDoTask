using System.Windows;
using System.Windows.Controls;
using ToDoTask.Interfaces;

namespace ToDoTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository _repository;

        public MainWindow(IRepository repository)
        {
            InitializeComponent();
            _repository = repository;
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
    }
}