using Handbook.Localization;
using Handbook.Mapping;
using Handbook.Services.Implementation;
using Handbook.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Resources;

namespace Handbook;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddAutoMapper(typeof(MappingProfile));
        serviceCollection.AddHttpClient();
        serviceCollection.AddScoped<IApiService, ApiService>();
        serviceCollection.AddSingleton(new ResourceManager("Handbook.Resources.Strings", typeof(Resources.Strings).Assembly));
        serviceCollection.AddSingleton<ILocalizationManager, LocalizationManager>();
        serviceCollection.AddSingleton<MainWindow>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var app = new App(serviceProvider);
        app.Run();
    }
}
