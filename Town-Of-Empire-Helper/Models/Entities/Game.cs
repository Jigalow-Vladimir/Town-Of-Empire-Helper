using System.Data;

namespace Town_Of_Empire_Helper.Entities
{
    public class Game
    {
        public List<Role> Roles { get; set; }
        public int CurrentDay { get; set; }

        public Game() =>
            Roles = [];

        public void Act()
        {
            foreach (Steps step in Enum.GetValues(typeof(Steps)))
            {
                Roles.Where(r => r.Acts.ContainsKey(step) && 
                    r.Acts[step].IsReady != false)
                    .ToList().ForEach(r => r.Acts[step].Invoke());
            }
        }
    }
}
