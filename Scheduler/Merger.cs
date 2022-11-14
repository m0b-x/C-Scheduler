namespace Scheduler
{
    public interface Merger
    {
        public bool Merge(ref Instructiune i1, ref Instructiune i2);

        public bool EsteMergeCase(Instructiune i1, Instructiune i2);
    }
}