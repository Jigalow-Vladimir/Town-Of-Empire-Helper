namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class PriorityItem<T>
    {
        public T? Value { get; set; }
        public Priority Priority { get; set; }
        public GameTime? EndTime { get; set; }

        public PriorityItem(T? value, Priority priority, GameTime? endTime) =>
            (Value, Priority, EndTime) = (value, priority, endTime);
    }
}
