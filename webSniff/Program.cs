using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace webSniff
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
            Application.Run(new MainForm());
        }
    }
}
//错误	1	Program 'f:\开发\测试开发\网络检查\webSniff\webSniff\obj\Debug\webSniff.exe' has more than one entry point defined: 'webSniff.Program.Main()'.  Compile with /main to specify the type that contains the entry point.	F:\开发\测试开发\网络检查\webSniff\webSniff\Program.cs	14	21	webSniff
