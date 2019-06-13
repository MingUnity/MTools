namespace ApkResignTool
{
    public interface IApkResignController
    {
        IApkResignView View { get; set; }

        void PickKeystore();

        void SavePassword();

        void SaveAlias();

        void PickJdkDir();

        void PickApk();

        void HandleDragFile(string[] filePath);

        void Sign();

        void AddDll();

        void CreateKeystore();
    }
}
