using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Entities;

namespace Town_Of_Empire_Helper.ViewModels.ActVMs
{
    public class TargetlessActVM : ActVM
    {
        public bool IsReady
        {
            get => _act.IsReady ?? false;
            set => _act.IsReady = value;
        }

        public TargetlessActVM(Game game, Act act) :
            base(game, act)
        { }

        public override void UpdateAll()
        {
            base.UpdateAll();
            OnPropertyChanged(nameof(IsReady));
        }
    }
}
