using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Models;

namespace Town_Of_Empire_Helper.Roles.City
{
    public class Escort : Role
    {
        private static List<string> _untouchables =  
           ["эскорт", "ведьма", "перевозчик", "ветеран", 
            "консорт", "тюремщик"];

        public Escort()
        {
            RoleConfigurationHandler.Configurate("эскорт", this);
            RegisterAct(Steps.TargetRedefine, "трахнуть", Logic, [new()]);
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            Acts[Steps.TargetRedefine].IsReady = null;
            Acts[Steps.TargetRedefine].Targets[0].Role = null;

            return string.Empty;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null)
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated ||
                _untouchables.Contains(tg.Name))
                return "цель вне зоны доступа";
            
            tg.Statuses[StatusType.Blocked]
                .Activate(this, GameTime.UpdateTime(Time));
            
            foreach (var act in tg.Acts)
                act.Value.IsReady = false;

            if (tg.Name == "маньяк")
            {
                Guests.Add(tg);
                if (tg.Stats["атака"].Get() > Stats["защита"].Get())
                    Statuses[StatusType.Killed].Activate(tg, null);
            }

            return string.Empty;
        }
    }
}
