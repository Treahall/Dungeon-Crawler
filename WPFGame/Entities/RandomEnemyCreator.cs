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
        Random random = new Random();
        int numEnemies = 3;
        private Player user;

        public RandomEnemyCreator(Player p)
        {
            user = p;
        }
        public Enemy getEnemy()
        {
            int enemyNumber = random.Next(1, numEnemies-1);

            switch (enemyNumber)
            {
                case 1:
                    return new WereWolf(){theUser = user};
                case 2:
                    return new Hound(){theUser = user};
                default:
                    return new Mandrake();
            }
        }
    }
}
