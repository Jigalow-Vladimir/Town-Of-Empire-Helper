namespace Town_Of_Empire_Helper.Entities.RoleInfo
{
    public class Info<T>
    {
        public T? Value { get; set; }
        public Priority Priority { get; set; }
        public GameTime? EndTime { get; set; }

        public Info(T? value, Priority priority, GameTime? endTime) =>
            (Value, Priority, EndTime) = (value, priority, endTime);
    }
}
