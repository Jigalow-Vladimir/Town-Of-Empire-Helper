using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Roles
{
    public class Bodyguard : Role
    {
        private List<Role> _tgGuests;

        public Bodyguard()
        {
            _tgGuests = [];

            RoleConfigurationHandler.Configurate("телохранитель", this);
            RegisterAct(Steps.Baffs, "защитить", Logic1, [new()]);
            RegisterAct(Steps.Kills, "убить", Logic2, []);
        }

        private string Logic1(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null) 
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated) 
                return "цель вне зоны доступа";

            _tgGuests = tg.Guests
                .Where(guest => 
                    guest.Stats["атака"].Get() > 0 && 
                    guest != this)
                .ToList();

            tg.Statuses[StatusType.Defended].Activate(
                activator: this,
                endTime: new(Time.Day + 1, Steps.Update));

            tg.Stats["защита"].Add(
                value: 2, 
                priority: Priority.Medium, 
                endTime: new (Time.Day + 1, Steps.Update));
            
            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_tgGuests.Count == 0) 
                return string.Empty;

            foreach (var guest in _tgGuests)
            {
                if (Stats["атака"].Get() > guest.Stats["защита"].Get())
                    guest.Statuses[StatusType.Killed].Activate(
                        activator: guest, 
                        endTime: null);
            }

            if (Statuses[StatusType.Healed].IsActivated == false)
                _tgGuests.ForEach(guest => 
                    Statuses[StatusType.Killed].Activate(
                        activator: guest, 
                        endTime: null));

            return string.Empty;
        }
    }
}
