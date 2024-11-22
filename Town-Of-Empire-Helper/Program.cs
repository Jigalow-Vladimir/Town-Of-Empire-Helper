﻿using Town_Of_Empire.Entities;
using Town_Of_Empire.Roles;
using Town_Of_Empire.Roles.City;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Town_Of_Empire
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sheriff = new Sheriff();
            var maniac = new Maniac();
            var bodyguard = new Bodyguard();
            var escort = new Escort();
            var consort = new Consort();

            sheriff.Acts[GameSteps.Other].Targets[0].Role = null;
            bodyguard.Acts[GameSteps.Baffs].Targets[0].Role = null;
            maniac.Acts[GameSteps.Kills].Targets[0].Role = null;
            escort.Acts[GameSteps.TargetRedefine].Targets[0].Role = maniac;
            consort.Acts[GameSteps.TargetRedefine].Targets[0].Role = maniac;

            List<Role> roles = [sheriff, maniac, bodyguard, escort, consort];
            foreach (var role in roles)
            {
                foreach (var act in role.Acts)
                {
                    act.Value.IsReady = true;
                }
            }

            foreach (var role1 in roles)
            {
                foreach (var role in roles)
                {
                    foreach (var act in role.Acts)
                    {
                        foreach (var tg in act.Value.Targets)
                        {
                            if (role1 == tg.Role) role1.Guests.Add(role);
                        }
                    }
                }
            }

            var game = new Game();
            game.Roles = roles;
            game.Act();

            foreach (var role in roles)
            {
                string result = $"Действия ({role.Name}):\n";
                
                foreach (var act in role.Acts)
                    if (act.Value.Result != string.Empty)
                        result += $"- {act.Value.Result}\n";

                result += "\nСтатусы:\n";
                foreach (var status in role.Statuses)
                    if (status.Value.IsActivated)
                    {
                        result += $"- {RoleConfigurationHandler
                            .Statuses[status.Value.Type]} -> ";
                        status.Value.IsActivatedBy.ForEach(t => result += $"{t.Name}; ");
                        result += "\n";
                    }
                Console.WriteLine(result);
                Console.WriteLine("----------------------");
            }
        }
    }
}