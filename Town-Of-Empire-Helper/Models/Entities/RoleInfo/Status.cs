namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class Status
    {
        public StatusType Type { get; set; }
        public bool IsActivated { get; set; }
        public List<Role> IsActivatedBy { get; set; }
        public GameTime? DeactivateTime { get; set; }

        public Status(StatusType type) =>
            (Type, IsActivated, IsActivatedBy) = (type, false, []);

        public void Activate(Role activator, GameTime? endTime)
        {
            IsActivated = true;
            IsActivatedBy.Add(activator);
            DeactivateTime = endTime;
        }

        public void Update(GameTime gameTime)
        {
            if (DeactivateTime != null && DeactivateTime.Equals(gameTime))
            {
                DeactivateTime = null;
                IsActivated = false;
                IsActivatedBy.Clear();
            }
        }
    }
}
