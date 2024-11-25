using System.Collections.ObjectModel;
using Town_Of_Empire_Helper.Entities;
using Town_Of_Empire_Helper.Roles;

namespace Town_Of_Empire_Helper.ViewModels
{
    public class GameVM
    {
        private Game _game;
        public ObservableCollection<PlayerVM> Players { get; private set; }

        public GameVM()
        {
            _game = new Game()
            { 
                Roles = new List<Role>()
                {
                    new Maniac(),
                    new Patrol(),
                    new Bodyguard(),
                    new Jailer()
                }
            };

            for (int i = 0; i < _game.Roles.Count; i++)
            {
                _game.Roles[i].User.Number = i + 1;
            }

            Players = new ObservableCollection<PlayerVM>();
            _game.Roles.ForEach(role => Players.Add(new (_game, role)));
        }
    }

    
}
