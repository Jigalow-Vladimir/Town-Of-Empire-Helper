namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class PriorityItem<T>
    {
        public T? Value { get; set; }
        public Priority Priority { get; set; }
        public int? EndDay { get; set; }

        public PriorityItem(T? value, Priority priority, int? endTime) =>
            (Value, Priority, EndDay) = (value, priority, endTime);
    }
}
