namespace NewFindACouple
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int result = 0;
            bool check = int.TryParse(args[0], out result);
            if (!check)
            {
                throw new IncorectNumberException();
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(result));
        }
    }
}