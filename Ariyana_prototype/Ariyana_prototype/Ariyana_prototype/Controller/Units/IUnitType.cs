using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ariyana_prototype
{
    abstract class IUnitType
    {
        public abstract void spawnCommand();
        public abstract void moveCommand(Unit obj);
        public abstract void attackCommand(Unit obj);
        public abstract void haltCommand();
        public abstract void waitCommand();
        public abstract void dieCommand();
    }
}
