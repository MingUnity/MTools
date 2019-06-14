using System;
using System.Windows.Forms;

namespace ReferenceReplaceTool
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

            ReferenceReplacerView view = new ReferenceReplacerView();

            IRefReplacerController ctrl = new RefReplacerController(view);

            Application.Run(view);
        }
    }
}
