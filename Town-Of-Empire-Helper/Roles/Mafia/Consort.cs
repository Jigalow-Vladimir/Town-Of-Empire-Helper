using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Roles
{
    public class Consort : Role
    {
        public Consort()
        {
            RoleConfigurationHandler.Configurate("консорт", this);
            Acts.Add(GameSteps.TargetRedefine, new Act(Logic, "трахнуть", [new()]));
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            Acts[GameSteps.TargetRedefine].IsReady = null;
            Acts[GameSteps.TargetRedefine].Targets[0].Role = null;

            return string.Empty;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null)
                return string.Empty;
            if (tg.Statuses[StatusType.InPrison].IsActivated ||
                tg.Name == "эскорт" ||
                tg.Name == "ведьма" ||
                tg.Name == "перевозчик" ||
                tg.Name == "ветеран" ||
                tg.Name == "консорт")
                return "цель вне зоны доступа";

            tg.Statuses[StatusType.Blocked]
                .Activate(this, new GameTime(Time.Day + 1, GameSteps.Update));
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
