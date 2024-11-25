using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Entities;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;

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

        public override void UpdateAll()
        {
            base.UpdateAll();
            OnPropertyChanged(nameof(Targets));
            foreach (var target in Targets)
                target.UpdateAll();
        }
    }
}
