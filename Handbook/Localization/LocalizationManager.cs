using Handbook.Localization;
using Handbook.Resources;
using System.ComponentModel;
using System.Globalization;

namespace Handbook;

public class LocalizationManager : INotifyPropertyChanged, ILocalizationManager
{
    private static LocalizationManager _instance;

    public static LocalizationManager Instance => _instance ??= new LocalizationManager();

    public event PropertyChangedEventHandler PropertyChanged;

    public string this[string key] => Strings.ResourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);

    public void SetRussianCulture()
    {
        SetCulture(Constants.Culture.Russian);
    }

    public void SetEnglishCulture()
    {
        SetCulture(Constants.Culture.English);
    }

    private void SetCulture(string cultureCode)
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
