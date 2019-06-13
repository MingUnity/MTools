using System;
using System.Windows.Forms;

namespace SlnBatchBuildTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            SlnBatchBuildView view = new SlnBatchBuildView();

            ISlnBatchBuildController controller = new SlnBatchBuildController(view);

            Application.Run(view);
        }
    }
}
