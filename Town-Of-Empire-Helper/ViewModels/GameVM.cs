using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Roles;
using Town_Of_Empire_Helper.ViewModels.Commands;

namespace Town_Of_Empire_Helper.ViewModels
{
    public class GameVM : INotifyPropertyChanged
    {
        private Game _game;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<PlayerVM> Players { get; private set; }
        public ICommand UpdateRolesCommand { get; private set; }
        public string CurrentDay => _game.CurrentDay.ToString("00");

        public GameVM()
        {
            UpdateRolesCommand = new RelayCommand(UpdateRoles);

            _game = new Game()
            { 
                Roles = new List<Role>()
                {
                    new Maniac(),
                    new Patrol(),
                    new Bodyguard(),
                    new Jailer()
                }
            };

            for (int i = 0; i < _game.Roles.Count; i++)
                _game.Roles[i].User.Number = i + 1;

            Players = new ObservableCollection<PlayerVM>();
            _game.Roles.ForEach(role => Players.Add(new (_game, role)));
        }

        private void UpdateRoles(object? parameter)
        {
            OnPropertyChanged(nameof(CurrentDay));
            _game.Update();
            foreach (var player in Players)
                player.UpdateAll();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
