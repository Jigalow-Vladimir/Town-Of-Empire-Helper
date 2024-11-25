using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Models;

namespace Town_Of_Empire_Helper.Roles
{
    public class Patrol : Role
    {
        public Patrol()
        {
            RoleConfigurationHandler.Configurate("дозорный", this);
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

            var result = "Посетители:\n";
            if (targets.Count <= 3) 
                foreach (var guest in tg.Guests)
                    result += $"-\t{guest.User.Nickname} " +
                        $"({guest.User.Number})\n";
            else
            {
                var rand = new Random();
                int guestIndex;
                for (int i = 0; i < 3; i++)
                {
                    var excludeIndexs = new List<int>();
                    do 
                    {
                        guestIndex = rand.Next(targets.Count);
                    }
                    while (excludeIndexs.Contains(guestIndex));
                    excludeIndexs.Add(guestIndex);

                    result += $"-\t{tg.Guests[guestIndex].User.Nickname} " +
                        $"({tg.Guests[guestIndex].User.Number})\n";
                }
            }
            
            return result;
        }
    }
}
