using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Models;

namespace Town_Of_Empire_Helper.Roles
{
    public class Sheriff : Role
    {
        public Sheriff() 
        {
            RoleConfigurationHandler.Configurate("шериф", this);
            RegisterAct(Steps.Other, "проверить", Logic, [new()]);
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            Acts[Steps.Other].IsReady = null;
            Acts[Steps.Other].Targets[0].Role = null;

            return string.Empty;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;
            
            if (tg == null) 
                return string.Empty;
            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            return tg.Stats["подозрительность"].Get() != 0 ? 
                $"->{tg.User.Number}: +" : 
                $"->{tg.User.Number}: -";
        }
    }
}