namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class Status
    {
        public int? endDay { get; set; }
        public bool IsActivated { get; set; }
        public StatusType Type { get; set; }
        public List<Role> IsActivatedBy { get; set; }

        public Status(StatusType type) =>
            (Type, IsActivated, IsActivatedBy) = (type, false, []);

        public void Activate(Role activator, int? endDay)
        {
            IsActivated = true;
            IsActivatedBy.Add(activator);
            this.endDay = endDay;
        }

        public void Deactivate()
        {
            endDay = null;
            IsActivated = false;
            IsActivatedBy.Clear();
        }

        public void Update(int day)
        {
            if (endDay != null && endDay == day)
            {
                Deactivate();
            }
        }
    }
}
