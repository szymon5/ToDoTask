using Microsoft.EntityFrameworkCore;
using ToDoTask.Models;
using ToDoTask.Repositories;
using ToDoTask.Services;
using ToDoTask.ViewModel;

namespace ToDoTask.Tests
{
    public class ToDoList_Test
    {
        private MainWindowViewModel _mainWindowViewModel;
        private AddNewTaskViewModel _addNewTaskViewModel;
        private EditRemoveViewModel _editRemoveTaskViewModel;
        private ApplicationDbContext _context;
        private Repository _repository;
        private int tasksAmount = 10;
        private DateTime currentDate;

        [SetUp]
        public void Setup()
        {
            currentDate = DateTime.Now;

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("todolist");

            var context = new ApplicationDbContext(builder.Options);

            var singleTasks = Enumerable.Range(1, tasksAmount).Select(i => new SingleTask
            {
                Id = i,
                Title = (i + i).ToString(),
                Description = (i + i * 2).ToString(),
                Day = currentDate
            });

            context.SingleTasks.AddRange(singleTasks);

            context.SaveChanges();
            _context = context;

            _repository = new Repository(_context);
            _mainWindowViewModel = new MainWindowViewModel(_repository);
            _addNewTaskViewModel = new AddNewTaskViewModel(_repository);
            _editRemoveTaskViewModel = new EditRemoveViewModel(_repository, 1);
        }

        [Test]
        public void GetAllTasks_WhenCalledAfterAddingTask_TheListShouldNotBeEmpty()
        {
            var tasksCountBeforeGettingList = _mainWindowViewModel.SingleTasks.Count;

            _mainWindowViewModel.GetAllTasks();

            var tasksCountAfterGettingList = _mainWindowViewModel.SingleTasks.Count;

            Assert.Multiple(() =>
            {
                Assert.Greater(tasksCountAfterGettingList, tasksCountBeforeGettingList);
                Assert.That(tasksCountBeforeGettingList + tasksAmount, Is.EqualTo(tasksCountAfterGettingList));
            });
        }

        [Test]
        public void GetTasksByDay_WhenCalledFirstTime_TheListShouldShouldContainElementsFromTheSpecificDay()
        {
            _mainWindowViewModel.GetTasksByDay(currentDate);

            var tasks = _mainWindowViewModel.SingleTasks.Count;

            Assert.Greater(tasks, 0);
        }

        [Test]
        public void AddNewTask_WhenCalled_NewTaskMustBeInTheDatabaseAndList()
        {
            var tasksCountBeforeGettingList = _mainWindowViewModel.SingleTasks.Count;

            _addNewTaskViewModel.SetTaskDay(currentDate.ToString());

            var message = _addNewTaskViewModel.AddNewTask("this is some task...", "Unit test task...");

            _mainWindowViewModel.GetAllTasks();

            var tasksCountAfterGettingList = _mainWindowViewModel.SingleTasks.Count;

            Assert.Multiple(() =>
            {
                Assert.That(message, Does.Contain("New task has been added"));
                Assert.That(tasksCountBeforeGettingList + tasksAmount + 1, Is.EqualTo(tasksCountAfterGettingList));
            });
        }

        [Test]
        public void AddNewTaskWithoutTitle_WhenCalled_ShouldReturnError()
        {
            _addNewTaskViewModel.SetTaskDay(DateTime.Now.ToString());

            var error = _addNewTaskViewModel.AddNewTask("", "some description..");

            Assert.That(error, Does.Contain("You have to provide all data"));
        }

        [Test]
        public void AddNewTaskWithoutDescription_WhenCalled_ShouldReturnError()
        {
            _addNewTaskViewModel.SetTaskDay(DateTime.Now.ToString());

            var error = _addNewTaskViewModel.AddNewTask("something", "");

            Assert.That(error, Does.Contain("You have to provide all data"));
        }

        [Test]
        public void AddNewTaskWithoutDay_WhenCalled_ShouldReturnError()
        {
            _addNewTaskViewModel.SetTaskDay("");

            var error = _addNewTaskViewModel.AddNewTask("abc", "def");

            Assert.That(error, Does.Contain("You have to provide all data"));
        }

        [Test]
        public void EditTaskWithoutTitle_WhenCalled_ShouldReturnError()
        {
            _addNewTaskViewModel.SetTaskDay(DateTime.Now.ToString());

            var error = _editRemoveTaskViewModel.EditTask("", "zxcxxxxx34343");

            Assert.That(error, Does.Contain("You have to provide all data"));
        }

        [Test]
        public void EditTaskWithoutDescription_WhenCalled_ShouldReturnError()
        {
            _addNewTaskViewModel.SetTaskDay(DateTime.Now.ToString());

            var error = _editRemoveTaskViewModel.EditTask("zaqwsx", "");

            Assert.That(error, Does.Contain("You have to provide all data"));
        }

        [Test]
        public void EditTaskWithoutDay_WhenCalled_ShouldReturnError()
        {
            _addNewTaskViewModel.SetTaskDay("");

            var error = _editRemoveTaskViewModel.EditTask("asdfg", "qwerty");

            Assert.That(error, Does.Contain("You have to provide all data"));
        }

        [Test]
        public void RemoveTask_WhenCalled_TaskMustBeRemovedFromDb()
        {
            _editRemoveTaskViewModel = new EditRemoveViewModel(_repository, 1);

            var tasksCountBeforeGettingList = _mainWindowViewModel.SingleTasks.Count;

            var removed = _editRemoveTaskViewModel.RemoveTask();

            _mainWindowViewModel.GetAllTasks();

            var tasksCountAfterGettingList = _mainWindowViewModel.SingleTasks.Count;

            Assert.Multiple(() =>
            {
                Assert.IsTrue(removed);
                Assert.That(tasksCountBeforeGettingList + tasksAmount - 1, Is.EqualTo(tasksCountAfterGettingList));
            });
        }

        [Test]
        public void RemoveTaskWithProvidedWrongID_WhenCalled_MustReturnFalse()
        {
            _editRemoveTaskViewModel = new EditRemoveViewModel(_repository, 999);

            var removed = _editRemoveTaskViewModel.RemoveTask();

            Assert.IsFalse(removed);
        }
    }
}