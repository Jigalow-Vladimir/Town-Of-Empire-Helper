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
            RegisterAct(GameSteps.Baffs, "защитить", Logic1, [new()]);
            RegisterAct(GameSteps.Baffs, "убить", Logic2, []);
        }

        private string Logic1(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null) 
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated) 
                return "цель вне зоны доступа";

            _tgGuests = tg.Guests
                .Where(s => s.Stats["атака"].Get() > 0 && s != this).ToList();

            tg.Statuses[StatusType.Defended]
                .Activate(this, new GameTime(Time.Day + 1, GameSteps.Update));

            tg.Stats["защита"].Add(
                value: 2, 
                priority: 1, 
                endTime: new(
                    day: Time.Day + 1, 
                    step: GameSteps.Update));
            
            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_tgGuests.Count == 0) 
                return string.Empty;

            foreach (var guest in _tgGuests)
            {
                if (Stats["атака"].Get() > guest.Stats["защита"].Get())
                    guest.Statuses[StatusType.Killed]
                        .Activate(guest, null);
            }

            if (Statuses[StatusType.Healed].IsActivated == false)
                _tgGuests.ForEach(s => Statuses[StatusType.Killed]
                    .Activate(s, null));

            return string.Empty;
        }
    }
}
