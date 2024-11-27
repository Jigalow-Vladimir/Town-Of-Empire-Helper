using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;
using Town_Of_Empire_Helper.Models;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Maniac : Role
    {
        public Maniac() 
        {
            RoleConfigurationHandler.Configurate("маньяк", this);
            RegisterAct(Steps.Kills, "убить", Logic, [new()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Kills].IsReady = null;
            Acts[Steps.Kills].Targets[0].Role = null;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null) 
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";
            
            if (Stats["атака"].Get() > tg.Stats["защита"].Get())
                tg.Statuses[StatusType.Killed].Activate(this, null);
            else return "защита цели слишком высока";
            
            return string.Empty;
        }
    }
}
