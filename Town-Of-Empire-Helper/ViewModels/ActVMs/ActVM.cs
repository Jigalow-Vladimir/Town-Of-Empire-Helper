using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Entities;

namespace Town_Of_Empire_Helper.ViewModels.ActVMs
{
    public class ActVM
    {
        protected Game _game;
        protected Act _act;

        public string Name =>
            _act.Name;

        public ActVM(Game game, Act act) =>
            (_game, _act) = (game, act);
    }
}
