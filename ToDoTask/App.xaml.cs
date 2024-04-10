using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using ToDoTask.Interfaces;
using ToDoTask.Repositories;
using ToDoTask.Services;

namespace ToDoTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }


        protected override async void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory());

            Configuration = builder.Build();

            var x = Configuration.GetConnectionString("ToDoList");

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        //protected override async void OnExit(ExitEventArgs e)
        //{
        //    base.OnExit(e);

        //    await AppHost!.StopAsync();
        //}

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ApplicationDbContext, ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=todolist;Integrated Security=True;Trust Server Certificate=True"));
            services.AddTransient(typeof(MainWindow));
            services.AddSingleton<IRepository, Repository>();
            //services.AddSingleton<SingleTaskViewModel, SingleTaskViewModel>();
            //services.AddSingleton<AddSingleTaskCommand, AddSingleTaskCommand>();
        }
    }

}
