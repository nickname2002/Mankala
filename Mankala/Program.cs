namespace Mancala
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Window screen = new Window();
            Application.Run(screen);
        }
    }
}