﻿using Town_Of_Empire.Entities;
using Town_Of_Empire.Entities.RoleInfo;

namespace Town_Of_Empire.Roles
{
    public class Maniac : Role
    {
        public Maniac() 
        {
            RoleConfigurationHandler.Configurate("маньяк", this);
            Acts.Add(GameSteps.Kills, new Act(Logic, "убить", [new()]));
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            Acts[GameSteps.Kills].IsReady = null;
            Acts[GameSteps.Kills].Targets[0].Role = null;

            return string.Empty;
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
