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

        bool ShouldMovMerge = false;
        bool ShouldImmediateMerge = false;
        bool ShouldMovReabsorb = false;

        public void SetParams(bool movMerge, bool immediateMerge, bool movReabsorb)
        {
            ShouldMovMerge = movMerge;
            ShouldImmediateMerge = immediateMerge;
            ShouldMovReabsorb = movReabsorb;
        }

        public void OptimizeCode(Instructiune [] instructions)
        {

        }

    }
}
