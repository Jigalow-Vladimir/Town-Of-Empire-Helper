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
            RegisterAct(Steps.Start, "посадить", Logic1, [new()]);
            RegisterAct(Steps.Kills, "убить", Logic2, []);
            RegisterAct(Steps.End, "исход", Logic3, []);
        }

        public override string Update(List<Target> targets)
        {
            base.Update(targets);

            _prisoner = null;

            Acts[Steps.Start].IsReady = null;
            Acts[Steps.Start].Targets[0].Role = null;
            Acts[Steps.Kills].IsReady = false;
            Acts[Steps.End].IsReady = null;

            return string.Empty;
        }

        private string Logic1(List<Target> targets)
        {
            _prisoner = targets[0].Role;

            if (_prisoner == null) return string.Empty;

            _prisoner.Statuses[StatusType.InPrison]
                .Activate(this, GameTime.UpdateTime(Time));

            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_prisoner == null) 
                return string.Empty;

            if (_prisoner.Stats["защита"].Get() < Stats["атака"].Get())
                _prisoner.Statuses[StatusType.Killed].Activate(this, null);

            return string.Empty;
        }

        private string Logic3(List<Target> targets)
        {
            if (_prisoner == null) 
                return string.Empty;

            if (_prisoner.Name == "маньяк" &&
                !_prisoner.Statuses[StatusType.Killed].IsActivated &&
                (Stats["защита"].Get() < _prisoner.Stats["атака"].Get()))
            {
                Statuses[StatusType.Killed].Activate(_prisoner, null);
                return "вы услышали какой-то хруст";
            }

            return string.Empty;
        }
    }
}
