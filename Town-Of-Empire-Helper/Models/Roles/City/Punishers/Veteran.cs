using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Veteran : Role
    {
        private int _bullets = 3;

        public Veteran()
        {
            RoleConfigurationHandler.Configurate("ветеран", this);
            RegisterAct(Steps.Baffs, "готовность", Logic1, [], false);
            RegisterAct(Steps.Kills, "убить", Logic2, []);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Kills].IsReady = false;
        }

        private string Logic1(List<Target> targets)
        {
            if (_bullets <= 0)
                return string.Empty;

            Stats["атака"].Add(
                value: 2, 
                priority: Priority.Medium, 
                endDay: CurrentDay + 1);

            Stats["защита"].Add(
                value: 1, 
                priority: Priority.Medium, 
                endDay: CurrentDay + 1);

            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_bullets <= 0) 
                return string.Empty;

            _bullets--;
            foreach (var guest in Guests)
            {
                if (Stats["атака"].Get() > guest.Stats["защита"].Get())
                    guest.Statuses[StatusType.Killed].Activate(
                        activator: this, 
                        endDay: null);
            }
            return string.Empty;
        }
    }
}
