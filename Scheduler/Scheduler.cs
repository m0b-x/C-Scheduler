using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    internal class Scheduler
    {
        MovMerger movMerger = new MovMerger();
        ImmediateMerger immediateMerger = new ImmediateMerger();
        MovReabsorber movReabsorber = new MovReabsorber();

    }
}
