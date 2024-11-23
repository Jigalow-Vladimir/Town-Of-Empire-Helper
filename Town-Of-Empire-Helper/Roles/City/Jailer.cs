using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Roles.City
{
    public class Jailer : Role
    {
        private Role? _prisoner;

        public Jailer()
        {
            RoleConfigurationHandler.Configurate("тюремщик", this);
            Acts.Add(GameSteps.Start, new Act(Logic1, "посадить", [new()]));
            Acts.Add(GameSteps.Kills, new Act(Logic2, "убить", []));
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            Acts[GameSteps.Start].IsReady = null;
            Acts[GameSteps.Start].Targets[0].Role = null;

            Acts[GameSteps.Kills].IsReady = false;

            return string.Empty;
        }

        private string Logic1(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null)
                return string.Empty;

            tg.Statuses[StatusType.InPrison]
                .Activate(this, new GameTime(Time.Day + 1, GameSteps.Update));
            _prisoner = tg;

            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_prisoner == null) 
                return string.Empty;



            return string.Empty;
        }
    }
}
