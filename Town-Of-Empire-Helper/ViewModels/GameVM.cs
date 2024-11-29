using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Town_Of_Empire_Helper.Models;
using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Roles;
using Town_Of_Empire_Helper.ViewModels.Commands;

namespace Town_Of_Empire_Helper.ViewModels
{
    public class GameVM : INotifyPropertyChanged
    {
        private Game _game;
        private List<Action> _rolesActivities;
        private int _currentStep;
        
        public ICommand NextStepCommand { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<PlayerVM> Players { get; private set; }
        public string CurrentDay => _game.CurrentDay.ToString("00");
        
        public string AllMessages
        {
            get
            {
                string result = string.Empty;
                foreach (var role in _game.Roles)
                {
                    result += $"{role.User.Number:00} ({role.Name}):\n";
                    foreach (var act in role.Acts)
                        result += act.Value.Result + "\n";
                    foreach (var status in role.Statuses)
                        if (status.Value.IsActivated)
                            result += status.Value.Type.ToString() + "\n";
                }

                return result;
            }
        }

        public GameVM()
        {
            _currentStep = 0;
            _rolesActivities = [SpendNight, UpdateRoles,];
            NextStepCommand = new RelayCommand(NextStep);

            _game = new Game()
            {
                Roles = new List<Role>()
                {
                    new Patrol(),
                    new Pathfinder(),
                    new Sheriff(),
                    
                    new Bodyguard(),
                    
                    new Jailer(),

                    new Escort(),

                    new Consort(),

                    new Maniac(),
                }
            };

            for (int i = 0; i < _game.Roles.Count; i++)
                _game.Roles[i].User.Number = i + 1;

            Players = new ObservableCollection<PlayerVM>();
            _game.Roles.ForEach(role => Players.Add(new (_game, role)));
        }

        private void NextStep(object? parameter)
        {
            _rolesActivities[_currentStep].Invoke();
            _currentStep++;
            if (_currentStep == _rolesActivities.Count)
                _currentStep = 0;
            OnPropertyChanged(nameof(AllMessages));
        }

        private void UpdateRoles()
        {
            OnPropertyChanged(nameof(CurrentDay));
            _game.Update();
            foreach (var player in Players)
                player.UpdateAll();
        }

        private void SpendNight()
        {
            _game.Act();
            foreach (var player in Players)
                player.UpdateAll();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
