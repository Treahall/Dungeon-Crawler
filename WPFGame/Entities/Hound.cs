using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame.Entities
{
    class Hound : Enemy
    {
        public Hound() : base() { }

        public override void setSpeed()
        {
            //speed = int.Parse(enemydata.GetString("HoundSpeed"));
        }

        public override void SetVelocity()
        {
            throw new NotImplementedException();
        }
    }
}
