using System;
using System.Windows.Forms;

namespace ApkResignTool
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

            ApkResignView view = new ApkResignView();

            ApkResignController controller = new ApkResignController(new WinRar())
            {
                View = view
            };

            Application.Run(view);
        }
    }
}
