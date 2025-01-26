using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using Handbook.Localization;
using Handbook.Models;
using Handbook.Services.Interfaces;
using static Handbook.Constants;

namespace Handbook;

public partial class MainWindow : Window
{
    private readonly IApiService apiService;
    private readonly IMapper mapper;
    private readonly ILocalizationManager localizationManager;

    private bool isRussianInterface = true;

    public MainWindow(IApiService apiService, IMapper mapper, ILocalizationManager localizationManager)
    {
        this.apiService = apiService;
        this.mapper = mapper;
        this.localizationManager = localizationManager;
        DataContext = localizationManager;

        InitializeComponent();
        deviceGrid.DataContext = localizationManager;

        Loaded += MainWindow_Loaded;
        downloadButton.Click += DownloadButton_Click;
        deviceGrid.SelectionChanged += DeviceGrid_SelectionChanged;
        languageSwitchButton.Click += LanguageSwitchButton_Click;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {

        List<DeviceType> deviceTypes = null;
        try
        {
            var deviceTypesDto = await apiService.LoadDeviceTypes();
            deviceTypes = mapper.Map<List<DeviceType>>(deviceTypesDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        foreach (var deviceType in deviceTypes)
        {
            devicesComboBox.Items.Add(deviceType.Name);
        }

        devicesComboBox.SelectedIndex = 0; 
    }

    private async void DownloadButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedDevice = devicesComboBox.SelectedItem.ToString();

        // Словарь для сопоставления выбранного типа устройства и метода загрузки данных
        var deviceTypeHandlers = new Dictionary<string, Func<Task>>
        {
            { 
                DeviceTypes.Cylinders, async () => deviceGrid.ItemsSource = mapper.Map<List<Cylinder>> (await apiService.LoadCylinders()) 
            },
            { 
                DeviceTypes.Pumps, async () => deviceGrid.ItemsSource = mapper.Map<List<Pump>> (await apiService.LoadPumps()) 
            },
            { 
                DeviceTypes.Valves, async () => deviceGrid.ItemsSource = mapper.Map<List<Valve>> (await apiService.LoadValves()) 
            }
        };

        if (deviceTypeHandlers.TryGetValue(selectedDevice, out var handler))
        {
            try
            {
                await handler();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Неизвестный тип устройства.");
        }
    }

    private void DeviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (deviceGrid.SelectedItem is Pump selectedPump)
        {
            deviceTextBox.Text = selectedPump.ToString();
        }
        else if(deviceGrid.SelectedItem is Cylinder selectedCylinder)
        {
            deviceTextBox.Text = selectedCylinder.ToString();
        }
        else if(deviceGrid.SelectedItem is Valve selectedValve)
        {
            deviceTextBox.Text = selectedValve.ToString();
        }
    }

    private void LanguageSwitchButton_Click(object sender, RoutedEventArgs e)
    {
        if (isRussianInterface)
        {
            localizationManager.SetCulture(Culture.English);
            isRussianInterface = !isRussianInterface;
        }
        else
        {
            localizationManager.SetCulture(Culture.Russian);
            isRussianInterface = !isRussianInterface;
        }
    }
}