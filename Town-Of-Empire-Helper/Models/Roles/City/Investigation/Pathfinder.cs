using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Roles
{
    public class Pathfinder : Role
    {
        public Pathfinder()
        {
            RoleConfigurationHandler.Configurate("следопыт", this);
            RegisterAct(Steps.Other, "проверить", Logic, [new ()]);
        }

        public override void Update()
        {
            base.Update();

            Acts[Steps.Other].Targets[0].Role = null;
            Acts[Steps.Other].IsReady = null;
        }

        private string Logic(List<Target> targets)
        {
            var tg = targets[0].Role;
            if (tg == null)
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated)
                return "цель вне зоны доступа";

            var tgsResults = new HashSet<string>();
            foreach (var act in tg.Acts)
            {
                foreach (var target in act.Value.Targets)
                {
                    if (target.Role != null)
                        tgsResults.Add($"{tg.User.Number.ToString("00")} " +
                            $"({tg.User.Nickname}) -> " +
                            $"{target.Role.User.Number.ToString("00")} " +
                            $"({target.Role.User.Nickname})\n");
                }
            }

            string result = string.Empty;
            foreach (var tgsResult in tgsResults)
                result += tgsResult;
            
            return result;
        }
    }
}
