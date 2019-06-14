namespace SlnBatchBuildTool
{
    public interface ISlnBatchBuildController
    {
        void AddSln();

        void RemoveSln();

        void Build();

        void HandleDragFile(string[] files);

        void FocusSln();

        void SaveConcurrentCount();
    }
}
