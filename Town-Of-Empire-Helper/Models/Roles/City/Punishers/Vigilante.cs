using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Vigilante : Role
    {
        private int? _suicideDay = null;
        private int _bullets = 3;

        public Vigilante()
        {
            RoleConfigurationHandler.Configurate("линчеватель", this);
            RegisterAct(Steps.Kills, "убить", Logic, [new()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Kills].Targets[0].Role = null;
            Acts[Steps.Kills].IsReady = null;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (_suicideDay == CurrentDay + 1)
                Statuses[StatusType.Killed].Activate(
                    activator: this, 
                    endDay: null);

            if (tg == null || _bullets <= 0)
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            _bullets--;
            if (Stats["атака"].Get() > tg.Stats["защита"].Get())
            {
                tg.Statuses[StatusType.Killed].Activate(
                    activator: this, 
                    endDay: null);

                if (tg.OtherStats["команда"].Get() == "город")
                    _suicideDay = CurrentDay + 1;
            }
            else return "защита цели слишком высока";

            return string.Empty;
        }
    }
}
