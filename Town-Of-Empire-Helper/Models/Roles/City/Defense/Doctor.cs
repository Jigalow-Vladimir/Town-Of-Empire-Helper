using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles.City.Defense
{
    public class Doctor : Role 
    {
        private bool _healedHimself = false;

        public Doctor()
        {
            RoleConfigurationHandler.Configurate("доктор", this);
            
        }

        // Healing logic
        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;

            // A Doctor can`t heal himself twice
            if (tg == null || _healedHimself)
                return string.Empty;

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
