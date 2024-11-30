using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Bodyguard : Role
    {
        private List<Role>? _tgGuests;

        public Bodyguard()
        {
            _tgGuests = [];

            RoleConfigurationHandler.Configurate("телохранитель", this);
            RegisterAct(Steps.Baffs, "защитить", Logic1, [new()]);
            RegisterAct(Steps.Kills, "убить", Logic2, []);
        }

        public override void Update()
        {
            base.Update();

            _tgGuests = null;

            Acts[Steps.Baffs].IsReady = null;
            Acts[Steps.Baffs].Targets[0].Role = null;

            Acts[Steps.Baffs].IsReady = null;
        }

        // defense act logic
        private string Logic1(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null) 
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated) 
                return "цель вне зоны доступа";

            // attackers of the bodyguard's target become victims of his attack
            _tgGuests = tg.Guests.Where(g => g.Stats["атака"]
                .Get() > 0 && g != this).ToList();

            tg.Statuses[StatusType.Defended].Activate(
                activator: this, 
                endDay: CurrentDay + 1);

            // a Bodyguard increases the current defense value to 2 (powerful)
            tg.Stats["защита"].Add(
                value:2, 
                priority: Priority.Medium, 
                endDay: CurrentDay + 1);
            
            return string.Empty;
        }

        // attack logic
        private string Logic2(List<Target> targets)
        {
            if (_tgGuests == null || _tgGuests.Count == 0) 
                return string.Empty;

            // targets with low defense die
            foreach (var guest in _tgGuests.Where(g => Stats["атака"].Get() >
                g.Stats["защита"].Get()))
            {
                guest.Statuses[StatusType.Killed].Activate(
                    activator: guest,
                    endDay: null);
            }

            // after the action the Bodyguard dies unless healed
            if (Statuses[StatusType.Healed].IsActivated == false)
                Statuses[StatusType.Killed].Activate(
                    activator: this,
                    endDay: null);

            return string.Empty;
        }
    }
}
