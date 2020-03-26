namespace Chat.UI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.FrontServiceClient.Infrastructure;
    using Chat.UI.Helper;
    using Chat.UI.View;

    public class ChatViewModel : ViewModelBase
    {
        private readonly Window currentWindow;

        private readonly IFrontServiceClient frontServiceClient;

        private readonly UserDTO loggedUser;

        private readonly Timer updateMessageTimer;

        private string message;

        private UserDTO sendingTo;

        public ChatViewModel()
        {
        }

        public ChatViewModel(Window currentWindow, IFrontServiceClient frontServiceClient, UserDTO loggedUser)
        {
            this.currentWindow = currentWindow;
            this.frontServiceClient = frontServiceClient;
            this.loggedUser = loggedUser;

            this.currentWindow.Title = $"Чат {loggedUser.LastName} {loggedUser.FirstName} {loggedUser.MiddleName}";

            this.SendMessageCommand = new RelayCommand(_ => this.SendMessage());
            this.EditSettingsCommand = new RelayCommand(_ => this.EditSettings());

            currentWindow.Closing += this.CurrentWindowOnClosing;

            var sendAllUser = new UserDTO { Id = 0, LastName = "Всем" };
            this.ConnectedUsers.Add(sendAllUser);
            this.sendingTo = sendAllUser;

            this.updateMessageTimer = new Timer { Interval = int.Parse(ConfigurationManager.AppSettings["ChatReadingDelay"]), Enabled = true };
            this.updateMessageTimer.Elapsed += this.UpdateMessageTimerOnElapsed;
            this.updateMessageTimer.Start();
        }

        public ObservableCollection<MessageDTO> Messages { get; } = new ObservableCollection<MessageDTO>();

        public ObservableCollection<UserDTO> ConnectedUsers { get; } = new ObservableCollection<UserDTO>();

        public string Message
        {
            get => this.message;
            set
            {
                this.message = value;
                this.OnPropertyChanged();
            }
        }

        public UserDTO SendingTo
        {
            get => this.sendingTo;
            set
            {
                this.sendingTo = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand SendMessageCommand { get; }

        public ICommand EditSettingsCommand { get; }

        private void SendMessage()
        {
            if (this.sendingTo?.LastName == null)
            {
                MessageBox.Show(
                    "Выберите получателя",
                    "Ошибка при отправке сообщения",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (this.ConnectedUsers.Count == 1)
            {
                MessageBox.Show(
                    "Нет пользователей для отправки сообщений",
                    "Ошибка при отправке сообщения",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var sendingMessage = new MessageDTO
                                     {
                                         CreatedOn = DateTime.Now,
                                         UserFrom = this.loggedUser,
                                         MessageContent = this.message
                                     };
            if (this.sendingTo.Id == 0)
            {
                sendingMessage.MessageType = MessageTypeDTO.Public;
                sendingMessage.UserTo = this.loggedUser;
            }
            else
            {
                sendingMessage.MessageType = MessageTypeDTO.Private;
                sendingMessage.UserTo = this.sendingTo;
            }

            var operationStatusInfo = this.frontServiceClient.SendMessage(sendingMessage);

            if (operationStatusInfo.OperationStatus == OperationStatus.Fail)
                MessageBox.Show(operationStatusInfo.AttachedInfo, "Ошибка при отправке сообщения", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EditSettings()
        {
            var settingsView = new SettingsView();
            var settingsViewModel = new SettingsViewModel(settingsView);
            settingsView.DataContext = settingsViewModel;
            settingsView.ShowDialog();

            this.updateMessageTimer.Stop();
            this.updateMessageTimer.Interval = int.Parse(ConfigurationManager.AppSettings["ChatReadingDelay"]);
            this.updateMessageTimer.Start();
        }

        private void UpdateMessageTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateMessageList();
            this.UpdateLoggedUsers();
        }

        private void UpdateMessageList()
        {
            var messages = (List<MessageDTO>)this.frontServiceClient.GetAllMessages(this.loggedUser).AttachedObject;
            if (messages == null) return;
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.DataBind,
                (Action)(() =>
                                {
                                    foreach (var msg in messages)
                                    {
                                        if (this.Messages.All(m => m.Id != msg.Id))
                                            this.Messages.Add(msg);
                                    }
                                }));
        }

        private void UpdateLoggedUsers()
        {
            var users = ((List<UserDTO>)this.frontServiceClient.GetAllUsers().AttachedObject)?.Where(_ => _.Id != this.loggedUser.Id).ToList();
            if (users == null) return;
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.DataBind,
                (Action)(() =>
                                {
                                    foreach (var user in users)
                                    {
                                        if (this.ConnectedUsers.All(u => u.Id != user.Id))
                                            this.ConnectedUsers.Add(user);
                                    }
                                }));
        }

        private void CurrentWindowOnClosing(object sender, CancelEventArgs e)
        {
            this.updateMessageTimer.Stop();
        }
    }
}