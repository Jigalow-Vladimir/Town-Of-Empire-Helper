using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;
using Town_Of_Empire_Helper.Models;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Consort : Role
    {
        private static List<string> _untouchables =
           ["эскорт", "ведьма", "перевозчик", "ветеран",
            "консорт", "тюремщик"];

        public Consort()
        {
            RoleConfigurationHandler.Configurate("консорт", this);
            RegisterAct(Steps.TargetRedefine, "трахнуть", Logic, [new()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.TargetRedefine].IsReady = null;
            Acts[Steps.TargetRedefine].Targets[0].Role = null;
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
                .Activate(this, new GameTime(Time.Day + 1, Steps.Start));

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
