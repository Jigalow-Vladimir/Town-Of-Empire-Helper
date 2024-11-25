using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Entities;
using System.Collections.ObjectModel;

namespace Town_Of_Empire_Helper.ViewModels.ActVMs
{
    public class TargetOrientedActVM : ActVM
    {
        public ObservableCollection<TargetVM> Targets { get; set; }

        public TargetOrientedActVM(Game game, Act act) :
            base(game, act)
        {
            Targets = [];
            act.Targets.ForEach(target => Targets.Add(new(game, target)));
        }
    }
}
