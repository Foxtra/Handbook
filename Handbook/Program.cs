using System.Windows;

namespace Handbook;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var app = new App();
        var window = new MainWindow();
        app.Run(window);
    }
}
