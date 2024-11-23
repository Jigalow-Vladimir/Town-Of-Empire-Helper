namespace Town_Of_Empire_Helper.Entities
{
    public class GameTime
    {
        public int Day { get; set; }
        public GameSteps Step { get; set; }

        public GameTime(int day, GameSteps step) => 
            (Day, Step) = (day, step);

        public override bool Equals(object? obj)
        {
            var time = obj as GameTime;
            if (time == null) 
                return false;
            return ((time.Day == Day) && (time.Step == Step));
        }
    }
}
