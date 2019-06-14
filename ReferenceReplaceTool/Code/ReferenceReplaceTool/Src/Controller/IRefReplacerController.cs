namespace ReferenceReplaceTool
{
    public interface IRefReplacerController
    {
        void ReplaceRef();

        void AddCsproj();

        void ReduceCsproj();

        void AddDll();

        void ReduceDll();

        void HandleCsprojDrag(string[] files);

        void HandleDllDrag(string[] files);

        void FocusCsproj();

        void FocusDll();
    }
}
