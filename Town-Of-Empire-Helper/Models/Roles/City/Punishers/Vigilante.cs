using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Vigilante : Role
    {
        private int? suicideDay;

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

            if (suicideDay == Time.Day)
                Statuses[StatusType.Killed].Activate(this, null);

            if (tg == null)
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            if (Stats["атака"].Get() > tg.Stats["защита"].Get())
            {
                tg.Statuses[StatusType.Killed].Activate(this, null);
                if (tg.OtherStats["команда"].Get() == "город")
                    suicideDay = Time.Day + 1;
            }
            else return "защита цели слишком высока";

            return string.Empty;
        }
    }
}
