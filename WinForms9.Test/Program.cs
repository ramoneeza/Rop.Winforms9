using Rop.Winforms9.Helper;

namespace WinForms9.Test
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            WinFormsHelper.SetDefaultIcon(WinForms9.Test.Properties.Resources.IcoNetwork);
            Application.Run(new Form2());
        }
    }
}