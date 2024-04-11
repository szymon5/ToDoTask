using Microsoft.EntityFrameworkCore;
using ToDoTask.Repositories;
using ToDoTask.Services;
using ToDoTask.ViewModel;

namespace ToDoTask.Tests
{
    public  class EmptyList_Test
    {
        [Test]
        public void GetAllTasks_IfThereAreNoElemets_MustReturnAnEmptyList()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("todolist");

            ApplicationDbContext context = new ApplicationDbContext(builder.Options);

            Repository repository = new Repository(context);

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(repository);

            mainWindowViewModel.GetAllTasks();

            var tasks = mainWindowViewModel.SingleTasks.Count;

            Assert.IsTrue(tasks == 0);
        }
    }
}
