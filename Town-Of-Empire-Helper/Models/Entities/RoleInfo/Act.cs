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
            List<Target> targets)
        {
            Logic = logic;
            Name = name;
            Targets = targets;
            IsReady = false;
            Result = string.Empty;
        }

        public void Invoke() =>
            Result = IsReady == true ?
                Logic?.Invoke(Targets) ?? "" : "";

        public void Update() => 
            Result = string.Empty;
    }
}
