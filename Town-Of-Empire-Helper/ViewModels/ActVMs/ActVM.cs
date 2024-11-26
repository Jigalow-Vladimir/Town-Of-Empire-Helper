using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Entities;
using System.ComponentModel;

namespace Town_Of_Empire_Helper.ViewModels.ActVMs
{
    public class ActVM : INotifyPropertyChanged
    {
        protected Game _game;
        protected Act _act;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name =>
            _act.Name;

        public ActVM(Game game, Act act) =>
            (_game, _act) = (game, act);

        public virtual void UpdateAll()
        {
            OnPropertyChanged(nameof(Name));
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
