using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public interface Merger
    {
        public bool Merge(ref Instructiune i1, ref Instructiune i2);
        public bool IsMergeCase(Instructiune i1, Instructiune i2);
    }
}
