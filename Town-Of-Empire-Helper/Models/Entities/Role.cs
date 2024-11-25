using Town_Of_Empire_Helper.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Entities
{
    public class Role
    {
        public string Name { get; set; }
        public GameTime Time { get; set; }
        public User User { get; set; }
        public List<Role> Guests { get; set; }
        public Dictionary<string, Stat<int>> Stats { get; set; }
        public Dictionary<StatusType, Status> Statuses { get; set; }
        public Dictionary<string, Stat<string>> OtherStats { get; set; }
        public Dictionary<Steps, Act> Acts { get; set; } 
        
        public Role()
        {
            Name = string.Empty;
            User = new ();
            Time = new (0, 0);
            Guests = [];
            Stats = [];
            OtherStats = [];
            Statuses = [];
            Acts = [];

            Acts.Add(Steps.Update, new Act(Update, string.Empty, []));
        }

        public virtual string Update(List<Target> targets)
        {
            Time.Day++;

            foreach (var stat in Stats)
                stat.Value.Update(Time);
            foreach (var stat in OtherStats)
                stat.Value.Update(Time);
            foreach (var status in Statuses)
                status.Value.Update(Time);
            foreach (var act in Acts)
                act.Value.Update();

            return string.Empty;
        }

        protected void RegisterAct(
            Steps step, 
            string name, 
            Func<List<Target>, string> logic, 
            List<Target> targets,
            bool? isReady = null)
        {
            if (!Acts.ContainsKey(step)) 
                Acts.Add(step, new Act(logic, name, targets, isReady));
        }
    }
}
