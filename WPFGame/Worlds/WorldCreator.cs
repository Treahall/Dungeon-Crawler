using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGame.Entities;

namespace WPFGame.Worlds
{
    public abstract class WorldCreator
    {
        public Player user;

        public WorldCreator(Player User)
        {
            user = User;
        }

        public abstract World getWorld();

    }

    //product MenuWord is defined in another file
    public class MenuCreator : WorldCreator
    {
        public MenuCreator(Player User) : base(User) { }

        public override World getWorld()
        {
            return new MenuWorld() { WorldUser = user };
        }
    }
    //product FightingWorld is define in another file
    public class DungeonCreator : WorldCreator
    {
        public DungeonCreator(Player User) : base(User) { }

        public override World getWorld()
        {
            return new FightingWorld() { WorldUser = user };
        }
    }
}
