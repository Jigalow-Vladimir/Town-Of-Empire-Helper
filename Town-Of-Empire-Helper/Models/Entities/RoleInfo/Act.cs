namespace Town_Of_Empire_Helper.Entities.RoleInfo
{
    public class Act
    {
        public string Name { get; set; }
        public List<Target> Targets { get; set; }
        public Func<List<Target>, string>? Logic { get; set; }
        public bool? IsReady { get; set; }
        public string Result { get; set; }

        public Act(
            Func<List<Target>, string> logic, 
            string name, 
            List<Target> targets,
            bool? isReady = null)
        {
            Logic = logic;
            Name = name;
            Targets = targets;
            Result = string.Empty;
            IsReady = isReady;
        }

        public void Invoke() =>
            Result = Logic?.Invoke(Targets) ?? string.Empty;

        public void Update() => 
            Result = string.Empty;
    }
}
