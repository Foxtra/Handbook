using AutoMapper;
using Handbook.Mapping;
using System.Windows;

namespace Handbook;

public partial class App : Application
{    
    public static IMapper Mapper { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = config.CreateMapper();
    }
}
