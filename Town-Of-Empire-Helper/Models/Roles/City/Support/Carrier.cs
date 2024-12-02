using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Carrier : Role
    {
        public Carrier()
        {
            RoleConfigurationHandler.Configurate("перевозчик", this);
            RegisterAct(Steps.TargetRedefine1, "перевезти", Logic, [new (), new ()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.TargetRedefine1].IsReady = null;
            Acts[Steps.TargetRedefine1].Targets[0].Role = null;
            Acts[Steps.TargetRedefine1].Targets[1].Role = null;
        }

        private string Logic(List<Target> targets)
        {
            var target1 = targets[0].Role;
            var target2 = targets[1].Role;

            if (target1 == null || target2 == null)
                return string.Empty;

            if (target1.Statuses[StatusType.InPrison].IsActivated ||
                target2.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            target1.Guests
                .ForEach(guest => guest.Acts.ToList()
                .ForEach(act => act.Value.Targets
                .Where(target => target.Role == target1).ToList()
                .ForEach(target => target.Role = target2)));

            target2.Guests
                .ForEach(guest => guest.Acts.ToList()
                .ForEach(act => act.Value.Targets
                .Where(target => target.Role == target2).ToList()
                .ForEach(target => target.Role = target1)));

            return string.Empty;
        }
    }
}
