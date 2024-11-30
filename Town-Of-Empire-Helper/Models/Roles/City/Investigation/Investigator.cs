using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Investigator : Role
    {
        private Dictionary<Role, int> _targets = [];

        public Investigator()
        {
            RoleConfigurationHandler.Configurate("следователь", this);
            RegisterAct(Steps.Other, "проверить", Logic, [new()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Other].Targets[0].Role = null;
            Acts[Steps.Other].IsReady = null;
        }

        private string Logic(List<Target> targets)
        {
            string result = string.Empty;
            var tg = targets[0].Role;
            if (tg == null)
                return string.Empty;

            if (!_targets.ContainsKey(tg))
            {
                _targets.Add(tg, CurrentDay);
                result = tg.OtherStats["группа ролей"]
                    .Get() ?? string.Empty;
            }
            else if (CurrentDay - _targets[tg] > 1)
            {
                _targets[tg] = CurrentDay;
                result = tg.Name;
            }
            
            return string.Empty;
        }
    }
}
