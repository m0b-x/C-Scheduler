namespace Scheduler
{
    internal class Scheduler
    {
        private MovMerger movMerger = new();
        private ImmediateMerger immediateMerger = new();
        private MovReabsorber movReabsorber = new();

        private bool ShouldMovMerge = false;
        private bool ShouldImmediateMerge = false;
        private bool ShouldMovReabsorb = false;

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