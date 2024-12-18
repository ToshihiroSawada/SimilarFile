using NLog;

namespace SimilarFiles
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
            Application.Run(new SearchList());
        }
    }

    public static class GlobalVar
    {
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static bool PSKILL = false;

    }
}