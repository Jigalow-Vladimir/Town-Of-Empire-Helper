using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.ViewModels.ActVMs
{
    public class OneTargetOrientedActVM : ActVM
    {
        public TargetVM Target { get; set; }

        public OneTargetOrientedActVM(Game game, Act act) : 
            base(game, act) =>
            Target = new TargetVM(game, act.Targets[0]);
    }
}
