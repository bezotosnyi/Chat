namespace Chat.UI
{
    using System.Configuration;
    using System.Windows;

    using Autofac;
    
    using Chat.FrontServiceClient;
    using Chat.FrontServiceClient.Infrastructure;
    using Chat.RestClient;
    using Chat.RestClient.Infrastructure;
    using Chat.UI.View;
    using Chat.UI.ViewModel;

    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var address = $"{ConfigurationManager.AppSettings["IPAddress"]}:{ConfigurationManager.AppSettings["Port"]}";

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<FrontServiceClient>().As<IFrontServiceClient>();
            containerBuilder.Register(s => new ChatServiceClient(address)).As<IChatService>();
            var container = containerBuilder.Build();

            var frontServiceClient = container.Resolve<IFrontServiceClient>();
            var startupView = new LoginView();
            var startupViewModel = new LoginViewModel(startupView, frontServiceClient);
            startupView.DataContext = startupViewModel;
            startupView.Show();
        }
    }
}
