using Town_Of_Empire_Helper.Models.Entities.RoleInfo;

namespace Town_Of_Empire_Helper.Models.Entities
{
    public class Role
    {
        public string Name { get; set; }
        public int CurrentDay { get; set; }
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
            CurrentDay = 0;
            Guests = [];
            Stats = [];
            OtherStats = [];
            Statuses = [];
            Acts = [];
        }

        public virtual void Update()
        {
            CurrentDay++;

            foreach (var stat in Stats)
                stat.Value.Update(CurrentDay);
            foreach (var stat in OtherStats)
                stat.Value.Update(CurrentDay);
            foreach (var status in Statuses)
                status.Value.Update(CurrentDay);
            foreach (var act in Acts)
                act.Value.Update();
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
