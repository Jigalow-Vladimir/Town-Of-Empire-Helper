using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Doctor : Role 
    {
        private bool _healedHimself = false;

        public Doctor()
        {
            RoleConfigurationHandler.Configurate("доктор", this);
            RegisterAct(Steps.Baffs, "лечить", Logic, [new()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Baffs].Targets[0].Role = null;
            Acts[Steps.Baffs].IsReady = null;
        }

        // Healing logic
        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;

            // A Doctor can`t heal himself twice
            if (tg == null || _healedHimself)
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            tg.Stats["защита"].Add(
                value: 2,
                priority: Priority.High,
                endDay: CurrentDay + 1);

            tg.Statuses[StatusType.Healed].Activate(
                activator: this,
                endDay: CurrentDay + 1);

            return string.Empty;
        }
    }
}
