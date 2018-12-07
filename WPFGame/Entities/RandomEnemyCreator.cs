using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame.Entities
{
    public class RandomEnemyCreator
    {
        //theuser = user
        Random random;
        private Player user;

        public RandomEnemyCreator(Player p)
        {
            random = new Random();
            user = p;
        }
        public Enemy getEnemy()
        {
            int enemyNumber = random.Next(1, 4);

            switch (enemyNumber)
            {
                case 1:
                    return new WereWolf(){theUser = user};
                case 2:
                    return new Hound(){theUser = user};
                case 3:
                    return new Mandrake(){theUser = user};
                default:
                    return new Mandrake();
            }
        }
    }
}
