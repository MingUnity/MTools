namespace ReferenceReplaceTool
{
    public interface IRefReplacerView
    {
        IRefReplacerController Controller { get; set; }

        int[] CsprojSelectedIndex { get; set; }

        string[] CsprojListItems { set; }

        int[] DllSelectedIndex { get; set; }

        string[] DllListItems { set; }

        bool ReplaceEnabled { get; set; }

        string ReplaceContent { get; set; }
    }
}
