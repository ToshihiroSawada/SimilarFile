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

    public static class Globals
    {
        public static bool PSKILL { get; set; } = false;
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
    }

}