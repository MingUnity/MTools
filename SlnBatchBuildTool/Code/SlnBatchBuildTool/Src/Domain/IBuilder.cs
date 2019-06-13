namespace SlnBatchBuildTool
{
    public interface IBuilder
    {
        bool IsValid { get; }

        void Build(string sln);
    }
}
