﻿namespace Town_Of_Empire.Entities.RoleInfo
{
    public class User
    {
        public string Nickname { get; set; }
        public int Number { get; set; }

        public User() =>
            (Nickname, Number) = ("-", 0);
    }
}