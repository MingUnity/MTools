namespace SlnBatchBuildTool
{
    public interface IBuilder
    {
        bool IsValid { get; }

        void Build(string sln);

        void ConcurrentBuild(string[] sln, int maxConcurrentCount = 4);
    }
}
