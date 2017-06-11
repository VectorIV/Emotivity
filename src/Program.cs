using System;
using System.Windows.Forms;

namespace emotivityMain
{

    static class emotivity
    {
        // --------------------------------------------------------------------------------------------
        // Note: During debugging, if the images appeared unsmoothed, clean the solution and rebuild it
        // --------------------------------------------------------------------------------------------

        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            emotivityMainForm mainScreen = new emotivityMainForm();
            Application.Run();

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

    }
}
