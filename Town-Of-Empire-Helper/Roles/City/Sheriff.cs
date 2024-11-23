using Town_Of_Empire.Entities;
using Town_Of_Empire.Entities.RoleInfo;

namespace Town_Of_Empire.Roles
{
    public class Sheriff : Role
    {
        public Sheriff() 
        {
            RoleConfigurationHandler.Configurate("шериф", this);
            Acts.Add(GameSteps.Other, new Act(Logic, "проверить", [new()]));
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            Acts[GameSteps.Other].IsReady = null;
            Acts[GameSteps.Other].Targets[0].Role = null;

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