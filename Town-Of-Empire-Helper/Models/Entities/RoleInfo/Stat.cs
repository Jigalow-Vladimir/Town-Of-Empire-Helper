namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class Stat<T>
    {
        public string Name { get; set; }
        public List<PriorityItem<T>> Infos { get; set; }

        public Stat(string name, T value, Priority priority, int? endDay) =>
            (Name, Infos) = (name, [new PriorityItem<T>(value, priority, endDay)]);

        public T? Get()
        {
            var info = Infos.MaxBy(info => info.Priority);
            return info != null ? info.Value : default;
        }

        public void Add(T value, Priority priority, int? endDay) =>
            Infos.Add(new PriorityItem<T>(value, priority, endDay));

        public void Update(int day) =>
            Infos.RemoveAll(i => i.EndDay != null && i.EndDay.Equals(day));
    }
}
