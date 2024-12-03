using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Consigliere : Role
    {
        public Consigliere()
        {
            RoleConfigurationHandler.Configurate("консильери", this);
            RegisterAct(Steps.Other, "проверить", Logic, [new()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Other].IsReady = null;
            Acts[Steps.Other].Targets[0].Role = null;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;
            if (tg == null)
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            return tg.Name;
        }
    }
}
