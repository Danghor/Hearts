using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;

namespace HeartsClient
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(out T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Guid clientId;
        public Guid ClientId
        {
            get => clientId;
            set => OnPropertyChanged(out clientId, value);
        }

        public ICommand ConnectCommand { get; }

        readonly HubConnection connection;

        public MainViewModel()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hearts")
                .Build();

            connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            ConnectCommand = new RelayCommand(async () =>
            {
                connection.On<Guid>("AssignId", id => ClientId = id);
                await connection.StartAsync();
                await connection.InvokeAsync("RequestId");
            });
        }
    }
}
