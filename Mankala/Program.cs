namespace Mankala
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Window screen = new Window();
            Application.Run(screen);
        }
    }
}