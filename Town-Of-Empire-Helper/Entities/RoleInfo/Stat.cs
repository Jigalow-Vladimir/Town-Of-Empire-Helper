namespace Town_Of_Empire_Helper.Entities.RoleInfo
{
    public class Stat<T>
    {
        public string Name { get; set; }
        public List<Info<T>> Infos { get; set; }

        public Stat(string name, T value, int priority, GameTime? endTime) =>
            (Name, Infos) = (name, [new Info<T>(value, priority, endTime)]);

        public T? Get()
        {
            var info = Infos.MaxBy(info => info.Priority);
            return info != null ? info.Value : default;
        }

        public void Add(T value, int priority, GameTime? endTime) =>
            Infos.Add(new Info<T>(value, priority, endTime));

        public void Update(GameTime time) =>
            (Infos.Where(i => 
                    i.EndTime != null && 
                    i.EndTime.Equals(time))
                .ToList()).ForEach(i => Infos.Remove(i));
    }
}
