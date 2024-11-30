using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Jailer : Role
    {
        private Role? _prisoner = null;
        private bool _doMistake = false;

        public Jailer()
        {
            RoleConfigurationHandler.Configurate("тюремщик", this);
            RegisterAct(Steps.Start, "посадить", Logic1, [new()]);
            RegisterAct(Steps.Kills, "убить", Logic2, [], false);
            RegisterAct(Steps.End, "исход", Logic3, []);
        }

        public override void Update()
        {
            base.Update();

            _prisoner = null;

            Acts[Steps.Start].IsReady = null;
            Acts[Steps.Start].Targets[0].Role = null;
            Acts[Steps.Kills].IsReady = false;
            Acts[Steps.End].IsReady = null;
        }

        private string Logic1(List<Target> targets)
        {
            _prisoner = targets[0].Role;

            if (_prisoner == null) return string.Empty;

            _prisoner.Statuses[StatusType.InPrison].Activate(
                activator: this, 
                endDay: CurrentDay + 1);

            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_prisoner == null || _doMistake) 
                return string.Empty;

            if (_prisoner.Stats["защита"].Get() < Stats["атака"].Get())
                _prisoner.Statuses[StatusType.Killed].Activate(
                    activator: this, 
                    endDay: null);

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
                Statuses[StatusType.Killed].Activate(
                    activator: _prisoner, 
                    endDay: null);
                return "вы услышали какой-то хруст";
            }

            return string.Empty;
        }
    }
}
