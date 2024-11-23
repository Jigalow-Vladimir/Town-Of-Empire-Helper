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
            foreach (GameSteps step in Enum.GetValues(typeof(GameSteps)))
            {
                foreach (var role in Roles)
                    if (role.Acts.ContainsKey(step) && role.Acts[step].IsReady != false)
                        role.Acts[step].Invoke();
            }
        }
    }
}
