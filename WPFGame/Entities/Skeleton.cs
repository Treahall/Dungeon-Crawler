using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame.Entities
{
    class Skeleton : Enemy
    {
        public Skeleton() : base() { }

        public override void setAnimations()
        {
            throw new NotImplementedException();
        }

        public override void setSpeed()
        {
            //speed = int.Parse(enemydata.GetString("SkeletonSpeed"));
        }

        public override void SetVelocity()
        {
            throw new NotImplementedException();
        }
    }
}
