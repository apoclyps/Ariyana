using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ariyana_prototype
{
    abstract class IEnemyType
    {
        public abstract void spawnCommand();
        public abstract void moveCommand(Enemy obj);
        public abstract void attackCommand(Enemy obj);
        public abstract void haltCommand();
        public abstract void waitCommand();
        public abstract void dieCommand();
    }
}
