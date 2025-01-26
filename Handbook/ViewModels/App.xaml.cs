using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Handbook;

public partial class App : Application
{    
    public static IServiceProvider ServiceProvider { get; private set; }

    public App(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
