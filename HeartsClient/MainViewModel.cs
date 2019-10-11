using GameMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HeartsClient
{
    public class MainViewModel : INotifyPropertyChanged, IPlayer
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(out T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand StartGameCommand { get; }

        Game game;

        public MainViewModel()
        {
            StartGameCommand = new RelayCommand(() => game = AiGame.NewAiGame(this));
        }

        public void TellHand(IEnumerable<Card> hand)
        {
            throw new NotImplementedException();
        }

        public Task<Card> PlayCardAsync()
        {
            throw new NotImplementedException();
        }

        public void NotifyCardPlayed(Card card)
        {
            throw new NotImplementedException();
        }
    }
}
