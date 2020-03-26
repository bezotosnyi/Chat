namespace Chat.UI.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.FrontServiceClient.Infrastructure;
    using Chat.UI.Helper;
    using Chat.UI.View;

    public class RegistrationViewModel : ViewModelBase
    {
        private readonly Window currentWindow;

        private readonly IFrontServiceClient frontServiceClient;

        private string firstName;

        private string lastName;

        private string middleName;

        private GenderDTO gender;

        private DateTime dateOfBirthday = DateTime.Now;

        private string email;

        private string login;

        private string password;

        public RegistrationViewModel()
        {
        }

        public RegistrationViewModel(Window currentWindow, IFrontServiceClient frontServiceClient)
        {
            this.currentWindow = currentWindow;
            this.frontServiceClient = frontServiceClient;

            this.CancelCommand = new RelayCommand(_ => this.Cancel());
            this.RegistrationCommand = new RelayCommand(_ => this.RegistrationUser());
        }

        public string FirstName
        {
            get => this.firstName;
            set
            {
                this.firstName = value;
                this.OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                this.lastName = value;
                this.OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => this.middleName;
            set
            {
                this.middleName = value;
                this.OnPropertyChanged();
            }
        }

        public GenderDTO Gender
        {
            get => this.gender;
            set
            {
                this.gender = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime DateOfBirthday
        {
            get => this.dateOfBirthday;
            set
            {
                this.dateOfBirthday = value;
                this.OnPropertyChanged();
            }
        }

        public string Email
        {
            get => this.email;
            set
            {
                this.email = value;
                this.OnPropertyChanged();
            }
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

        public ICommand CancelCommand { get; }

        public ICommand RegistrationCommand { get; }

        private void Cancel()
        {
            this.currentWindow.Close();
        }

        private void RegistrationUser()
        {
            var operationStatus = this.frontServiceClient.Registration(new UserDTO
                                                                           {
                                                                               FirstName = this.firstName,
                                                                               LastName = this.lastName,
                                                                               MiddleName = this.middleName,
                                                                               Gender = this.gender,
                                                                               DateOfBirthday = this.dateOfBirthday,
                                                                               Email = this.email,
                                                                               Login = this.Login,
                                                                               Password = this.Password
                                                                           });

            if (operationStatus.OperationStatus == OperationStatus.Success)
            {
                MessageBox.Show(operationStatus.AttachedInfo, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new LoginView();
                var loginViewModel = new LoginViewModel(loginWindow, this.frontServiceClient)
                                         {
                                             Login = this.login, Password = this.Password
                                         };
                loginWindow.DataContext = loginViewModel;
                loginWindow.Show();
                this.currentWindow.Close();
            }
            else
                MessageBox.Show(operationStatus.AttachedInfo, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}