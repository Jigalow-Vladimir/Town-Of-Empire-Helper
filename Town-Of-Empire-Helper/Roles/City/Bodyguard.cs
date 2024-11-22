using Town_Of_Empire.Entities;
using Town_Of_Empire.Entities.RoleInfo;

namespace Town_Of_Empire.Roles
{
    public class Bodyguard : Role
    {
        private List<Role> _tgGuests;

        public Bodyguard()
        {
            _tgGuests = [];

            RoleConfigurationHandler.Configurate("телохранитель", this);
            Acts.Add(GameSteps.Baffs, new Act(Logic1, "защитить", [new()]));
            Acts.Add(GameSteps.Kills, new Act(Logic2, "убить", []));
        }

        private string Logic1(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null) 
                return string.Empty;
            if (tg.Statuses[StatusType.InPrison].IsActivated) 
                return "цель вне зоны доступа";

            _tgGuests = tg.Guests
                .Where(s => s.Stats["атака"].Get() > 0)
                .ToList();

            _tgGuests.Remove(this);

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
                    guest.Statuses[StatusType.Killed].Activate(guest, null);
            }

            if (Statuses[StatusType.Healed].IsActivated == false)
                _tgGuests.ForEach(s => Statuses[StatusType.Killed].Activate(s, null));

            return string.Empty;
        }
    }
}
