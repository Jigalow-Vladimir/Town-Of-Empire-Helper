using System.Collections.ObjectModel;
using System.ComponentModel;
using Town_Of_Empire_Helper.Models;
using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.ViewModels.ActVMs;

namespace Town_Of_Empire_Helper.ViewModels
{
    public class PlayerVM : INotifyPropertyChanged
    {
        private Game _game;
        private Role _role;
        private ObservableCollection<TargetlessActVM> _targetlessActs = [];
        private ObservableCollection<TargetOrientedActVM> _targetOrientedActs = [];
        private ObservableCollection<OneTargetOrientedActVM> _oneTargetOrientedActs = [];
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<TargetOrientedActVM> TargetOrientedActs => 
            _targetOrientedActs;

        public ObservableCollection<TargetlessActVM> TargetlessActs => 
            _targetlessActs;

        public ObservableCollection<OneTargetOrientedActVM> OneTargetOrientedActs =>
            _oneTargetOrientedActs;

        public string Number =>
            _role.User.Number.ToString("00");

        public string Role =>
            _role.Name;

        public string Nickname
        {
            get => _role.User.Nickname;
            set => _role.User.Nickname = value;
        }

        public PlayerVM(Game game, Role role)
        {
            (_game, _role) = (game, role);
            SetTargetOrientedActs();
            SetTargetlessActs();
            SetOneTargetOrientedActs();
        }

        private void SetTargetOrientedActs()
        {
            foreach (var act in _role.Acts
                .Where(act => act.Value.Targets.Count > 1 && 
                act.Value.IsReady == null))
            {
                _targetOrientedActs.Add(new (_game, act.Value));
            };
        }

        private void SetTargetlessActs()
        {
            foreach (var act in _role.Acts
                .Where(act => act.Value.Targets.Count == 0 &&
                    act.Value.IsReady != null))
            {
                _targetlessActs.Add(new(_game, act.Value));
            }
        }

        private void SetOneTargetOrientedActs()
        {
            foreach (var act in _role.Acts
                .Where(act => act.Value.Targets.Count == 1 && 
                act.Value.IsReady == null))
            {
                _oneTargetOrientedActs.Add(new(_game, act.Value));
            }
        }

        public void UpdateAll()
        {
            foreach (var act in TargetlessActs)
                act.UpdateAll();
            foreach (var act in TargetOrientedActs)
                act.UpdateAll();

            OnPropertyChanged(nameof(Nickname));
            OnPropertyChanged(nameof(Role));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
