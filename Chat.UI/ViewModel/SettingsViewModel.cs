namespace Chat.UI.ViewModel
{
    using System.Configuration;
    using System.Windows;
    using System.Windows.Input;

    using Chat.UI.Helper;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly Window currentWindow;

        private string ipAddress;

        private int port;

        private int chatReadingDelay;

        public SettingsViewModel()
        {
        }

        public SettingsViewModel(Window currentWindow)
        {
            this.currentWindow = currentWindow;

            this.ApplyCommand = new RelayCommand(_ => this.Apply());

            this.ipAddress = ConfigurationManager.AppSettings["IPAddress"];
            this.port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            this.chatReadingDelay = int.Parse(ConfigurationManager.AppSettings["ChatReadingDelay"]);
        }
        
        public string IPAddress
        {
            get => this.ipAddress;
            set
            {
                this.ipAddress = value;
                this.OnPropertyChanged();
            }
        }

        public int Port
        {
            get => this.port;
            set
            {
                this.port = value;
                this.OnPropertyChanged();
            }
        }

        public int ChatReadingDelay
        {
            get => this.chatReadingDelay;
            set
            {
                this.chatReadingDelay = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand ApplyCommand { get; }

        private void Apply()
        {
            MessageBox.Show(
                "Изменение значения ip адреса и порта вступят в силу только после перезапуска приложения!",
                "Внимание",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["IPAddress"].Value = this.ipAddress;
            config.AppSettings.Settings["Port"].Value = this.port.ToString();
            config.AppSettings.Settings["ChatReadingDelay"].Value = this.chatReadingDelay.ToString();
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");

            this.currentWindow.Close();
        }
    }
}