using WpfChatApp.MVVM.Core;
using WpfChatApp.Net;

namespace WpfChatApp.ViewModel
{

    public class MainViewModel
    {
        public RelayCommand ConnectToServerCommand { get; set; }

        public string Username { get; set; }


        private ServerConnection _serverConnection;

        public MainViewModel()
        {
            _serverConnection = new ServerConnection();
            ConnectToServerCommand = new RelayCommand(o => _serverConnection.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
        }



    }
}