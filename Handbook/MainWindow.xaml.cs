using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Handbook.Resources;

namespace Handbook;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    bool isRussianInterface = true;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded; // Вызов функции при загрузке окна
        downloadButton.Click += DownloadButton_Click; 
        deviceGrid.SelectionChanged += DeviceGrid_SelectionChanged; // Вызов функции при выделении элемента грида
        languageSwitchButton.Click += LanguageSwitchButton_Click; 
        deviceGrid.AutoGenerateColumns = false;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var apiUrl = @"https://2392bb8b-2589-4515-a05d-bff3882c6c75.mock.pstmn.io/devices";

        List<Device> data = null;
        try
        {
            var service = new ApiService();
            data = await service.GetDataAsync<List<Device>>(apiUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        // Заполнение ComboBox из кода
        foreach (var item in data)
        {
            devicesComboBox.Items.Add(item.Name);
        }

        // Установить выбранный элемент
        devicesComboBox.SelectedIndex = 0; // Выбор первого элемента
    }

    // Обработчик нажатия кнопки
    private async void DownloadButton_Click(object sender, RoutedEventArgs e)
    {
        var baseUrl = @"https://2392bb8b-2589-4515-a05d-bff3882c6c75.mock.pstmn.io/";

        var selectedDevice = devicesComboBox.SelectedItem.ToString();

        var finalUrl = $"{baseUrl}{selectedDevice}";

        switch (selectedDevice)
        {
            case "pumps":
                List<Pump> data1 = null;
                try
                {
                    var service = new ApiService();
                    data1 = await service.GetDataAsync<List<Pump>>(finalUrl);
                    deviceGrid.ItemsSource = data1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                break;
            case "cylinders":
                List<Cylinder> data2 = null;
                try
                {
                    var service = new ApiService();
                    data2 = await service.GetDataAsync<List<Cylinder>>(finalUrl);
                    deviceGrid.ItemsSource = data2;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                break;
            case "valves":
                List<Valve> data3 = null;
                try
                {
                    var service = new ApiService();
                    data3 = await service.GetDataAsync<List<Valve>>(finalUrl);
                    deviceGrid.ItemsSource = data3;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                break;
            default:
                break;
        }       
    }

    // Обработчик выделения элемента в DataGrid
    private void DeviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (deviceGrid.SelectedItem is Pump selectedPump)
        {
            // Отображаем дополнительную информацию
            deviceTextBox.Text = selectedPump.ToString();
        }
        else if(deviceGrid.SelectedItem is Cylinder selectedCylinder)
        {
            // Отображаем дополнительную информацию
            deviceTextBox.Text = selectedCylinder.ToString();
        }
        else if(deviceGrid.SelectedItem is Valve selectedValve)
        {
            // Отображаем дополнительную информацию
            deviceTextBox.Text = selectedValve.ToString();
        }
        else
        {
            // Если ничего не выбрано
            deviceTextBox.Text = "Выберите элемент...";
        }
    }

    // Обработчик нажатия кнопки для смены языка
    private void LanguageSwitchButton_Click(object sender, RoutedEventArgs e)
    {
        // Переключение на русский
        
        if (isRussianInterface)
        {
            LocalizationManager.Instance.SetCulture("en-US");
            isRussianInterface = !isRussianInterface;
        }
        else
        {
            LocalizationManager.Instance.SetCulture("ru-RU");
            isRussianInterface = !isRussianInterface;
        }
    }

    // Метод для смены языка
    private void SetLanguage(string cultureCode)
    {
        // Устанавливаем текущую культуру
        var culture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        // Обновить интерфейс
        InitializeComponent(); // Перезагрузить окно
    }
}

public class LocalizationManager : INotifyPropertyChanged
{
    private static LocalizationManager _instance;

    public static LocalizationManager Instance => _instance ??= new LocalizationManager();

    public event PropertyChangedEventHandler PropertyChanged;

    public string this[string key] => Strings.ResourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);

    public void SetCulture(string cultureCode)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);

        // Уведомление об изменении всех привязанных свойств
        OnPropertyChanged(string.Empty);
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T> GetDataAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode(); // Бросает исключение, если статус код не успешный

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Игнорирует регистр имен полей
        });
    }
}

public class Device
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}

public class Pump
{
    public required int Capacity { get; set; }
    public required double Head { get; set; }
    public required int Speed { get; set; }
    public required int Efficiency { get; set; }
    public required double MotorPower { get; set; }
    public required long Id { get; set; }
    public required Guid Uid { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"Capacity: {Capacity}, Head: {Head}, Speed: {Speed}, Efficiency: {Efficiency}, MotorPower: {MotorPower}, Uid: {Uid}";
    }
}

public class Cylinder
{
    public required int Stroke { get; set; }
    public required int Bore { get; set; }
    public required int OuterDiameter { get; set; }
    public required int ShrinkLength { get; set; }
    public required int ExtendLength { get; set; }
    public required int OilPortDistance { get; set; }
    public required long Id { get; set; }
    public required Guid Uid { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"Stroke: {Stroke}, Bore: {Bore}, OuterDiameter: {OuterDiameter}, ShrinkLength: {ShrinkLength}, ExtendLength: {ExtendLength}, OilPortDistance: {OilPortDistance}, Uid: {Uid}";
    }
}

public class Valve
{
    public required double NominalPipeSize { get; set; }
    public required int Globe { get; set; }
    public required int BallCheck { get; set; }
    public required int Angle { get; set; }
    public required int SwingCheck { get; set; }
    public required int PlugCock { get; set; }
    public required int GateOrBallValve { get; set; }
    public required long Id { get; set; }
    public required Guid Uid { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"NominalPipeSize: {NominalPipeSize}, Globe: {Globe}, BallCheck: {BallCheck}, Angle: {Angle}, SwingCheck: {SwingCheck}, PlugCock: {PlugCock}, GateOrBallValve: {GateOrBallValve}, Uid: {Uid}";
    }
}