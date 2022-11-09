namespace Scheduler
{
    internal class Scheduler
    {
        MovMerger movMerger = new();
        ImmediateMerger immediateMerger = new();
        MovReabsorber movReabsorber = new();

        bool ShouldMovMerge = false;
        bool ShouldImmediateMerge = false;
        bool ShouldMovReabsorb = false;

        public void SetParams(bool movMerge, bool immediateMerge, bool movReabsorb)
        {
            ShouldMovMerge = movMerge;
            ShouldImmediateMerge = immediateMerge;
            ShouldMovReabsorb = movReabsorb;
        }

        public void OptimizeCode(Instructiune[] instructions)
        {

        }

    }
}
