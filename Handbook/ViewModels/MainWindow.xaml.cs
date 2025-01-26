using System.Windows;
using System.Windows.Controls;
using Handbook.Localization;
using Handbook.Models;
using Handbook.Services.Implementation;
using Handbook.Services.Interfaces;
using static Handbook.Constants;

namespace Handbook;

public partial class MainWindow : Window
{
    private readonly IApiService _apiService;

    private bool isRussianInterface = true;

    public MainWindow()
    {
        InitializeComponent();

        _apiService = new ApiService();

        Loaded += MainWindow_Loaded;
        downloadButton.Click += DownloadButton_Click;
        deviceGrid.SelectionChanged += DeviceGrid_SelectionChanged;
        languageSwitchButton.Click += LanguageSwitchButton_Click;
        deviceGrid.AutoGenerateColumns = false;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {

        List<DeviceType> deviceTypes = null;
        try
        {
            var deviceTypesDto = await _apiService.LoadDeviceTypes();
            deviceTypes = App.Mapper.Map<List<DeviceType>>(deviceTypesDto);
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
                DeviceTypes.Cylinders, async () => deviceGrid.ItemsSource = App.Mapper.Map<List<Cylinder>> (await _apiService.LoadCylinders()) 
            },
            { 
                DeviceTypes.Pumps, async () => deviceGrid.ItemsSource = App.Mapper.Map<List<Pump>> (await _apiService.LoadPumps()) 
            },
            { 
                DeviceTypes.Valves, async () => deviceGrid.ItemsSource = App.Mapper.Map<List<Valve>> (await _apiService.LoadValves()) 
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
            LocalizationManager.Instance.SetEnglishCulture();
            isRussianInterface = !isRussianInterface;
        }
        else
        {
            LocalizationManager.Instance.SetRussianCulture();
            isRussianInterface = !isRussianInterface;
        }
    }
}