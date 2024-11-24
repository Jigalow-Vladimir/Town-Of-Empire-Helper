﻿using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Entities.RoleInfo;
using Town_Of_Empire_Helper.Models;

namespace Town_Of_Empire_Helper.Roles
{
    public class Bodyguard : Role
    {
        private List<Role> _tgGuests;

        public Bodyguard()
        {
            _tgGuests = [];

            RoleConfigurationHandler.Configurate("телохранитель", this);
            RegisterAct(Steps.Baffs, "защитить", Logic1, [new()]);
            RegisterAct(Steps.Kills, "убить", Logic2, []);
        }

        private string Logic1(List<Target> targets)
        {
            var tg = targets[0].Role;

            if (tg == null) 
                return string.Empty;

            if (tg.Statuses[StatusType.InPrison].IsActivated) 
                return "цель вне зоны доступа";

            _tgGuests = tg.Guests.Where(g => g.Stats["атака"]
                .Get() > 0 && g != this).ToList();

            tg.Statuses[StatusType.Defended]
                .Activate(this, GameTime.UpdateTime(Time));

            tg.Stats["защита"]
                .Add(2, Priority.Medium, GameTime.UpdateTime(Time));
            
            return string.Empty;
        }

        private string Logic2(List<Target> targets)
        {
            if (_tgGuests.Count == 0) 
                return string.Empty;

            foreach (var g in _tgGuests.Where(g => Stats["атака"].Get() > 
                g.Stats["защита"].Get()))
                g.Statuses[StatusType.Killed].Activate(g, null);

            if (Statuses[StatusType.Healed].IsActivated == false)
                _tgGuests.ForEach(g => Statuses[StatusType.Killed].Activate(g, null));

            return string.Empty;
        }
    }
}