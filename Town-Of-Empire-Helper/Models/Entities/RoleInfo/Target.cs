﻿namespace Town_Of_Empire_Helper.Models.Entities.RoleInfo
{
    public class Target
    {
        public Role? Role { get; set; }
        public string Name { get; set; }

        public Target() =>
            Name = "цель";
    }
}
