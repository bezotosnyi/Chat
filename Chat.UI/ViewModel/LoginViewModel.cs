namespace Chat.UI.ViewModel
{
    using System.Windows;
    using System.Windows.Input;

    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.FrontServiceClient.Infrastructure;
    using Chat.UI.Helper;
    using Chat.UI.View;

    public class LoginViewModel : ViewModelBase
    {
        private readonly Window currentWindow;

        private readonly IFrontServiceClient frontServiceClient;

        private string login;

        private string password;

        public LoginViewModel()
        {
        }

        public LoginViewModel(Window currentWindow, IFrontServiceClient frontServiceClient)
        {
            this.currentWindow = currentWindow;
            this.frontServiceClient = frontServiceClient;

            this.LoginCommand = new RelayCommand(_ => this.LoginUser());
            this.RegistrationCommand = new RelayCommand(_ => this.RegistrationUser());
        }

        public string Login
        {
            get => this.login;
            set
            {
                this.login = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get => this.password;
            set
            {
                this.password = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public ICommand RegistrationCommand { get; }

        private void LoginUser()
        {
            var operationStatus = this.frontServiceClient.Login(new UserLoginDTO(this.login, this.password));

            if (operationStatus.OperationStatus == OperationStatus.Success)
            {
                var chatView = new ChatView();
                var chatViewModel = new ChatViewModel(chatView, this.frontServiceClient, (UserDTO)operationStatus.AttachedObject);
                chatView.DataContext = chatViewModel;
                chatView.Show();
                this.currentWindow.Close();
            }
            else
                MessageBox.Show(operationStatus.AttachedInfo, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void RegistrationUser()
        {
            var registrationWindow = new RegistrationView();
            var registrationViewModel = new RegistrationViewModel(registrationWindow, this.frontServiceClient);
            registrationWindow.DataContext = registrationViewModel;
            registrationWindow.Show();
            this.currentWindow.Close();
        }
    }
}