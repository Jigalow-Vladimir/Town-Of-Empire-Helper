using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Entities;

namespace Town_Of_Empire_Helper.ViewModels.ActVMs
{
    public class TargetVM
    {
        private Target _target;
        private Game _game;

        public string Name =>
            _target.Name;

        public string Number
        {
            get => _target.Role == null ? string.Empty :
                _target.Role.User.Number.ToString("00");
            set
            {
                if (int.TryParse(value, out int number) &&
                    number - 1 > 0 &&
                    number - 1 < _game.Roles.Count)
                    _target.Role = _game.Roles[number - 1];
                else _target.Role = null;
            }
        }

        public TargetVM(Game game, Target target) =>
            (_game, _target) = (game, target);
    }
}
