namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class Stat<T>
    {
        public string Name { get; set; }
        public List<PriorityItem<T>> Infos { get; set; }

        public Stat(string name, T value, Priority priority, GameTime? endTime) =>
            (Name, Infos) = (name, [new PriorityItem<T>(value, priority, endTime)]);

        public T? Get()
        {
            var info = Infos.MaxBy(info => info.Priority);
            return info != null ? info.Value : default;
        }

        public void Add(T value, Priority priority, GameTime? endTime) =>
            Infos.Add(new PriorityItem<T>(value, priority, endTime));

        public void Update(GameTime time) =>
            Infos.RemoveAll(i => i.EndTime != null && i.EndTime.Equals(time));
    }
}
