using System.Data;
using Town_Of_Empire_Helper.Models.Entities;

namespace Town_Of_Empire_Helper.Models
{
    public class Game
    {
        public List<Role> Roles { get; set; }
        public int CurrentDay { get; set; }

        public Game() =>
            Roles = [];

        public void Update()
        {
            Roles.ForEach(role => role.Update());
            CurrentDay++;
        }

        public void Act()
        {
            foreach (Steps step in Enum.GetValues(typeof(Steps)))
            {
                foreach (var role in Roles
                    .Where(r => r.Acts
                    .ContainsKey(step) && r.Acts[step].IsReady != false))
                {
                    role.Acts[step].Invoke();
                }
            }
        }
    }
}
