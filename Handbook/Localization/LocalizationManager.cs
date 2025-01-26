using Handbook.Localization;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Handbook;

public class LocalizationManager : ILocalizationManager, INotifyPropertyChanged
{
    private readonly ResourceManager _resourceManager;

    private CultureInfo _currentCulture;

    public LocalizationManager(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
        CurrentCulture = CultureInfo.CurrentCulture;
    }

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        private set
        {
            if (_currentCulture != value)
            {
                _currentCulture = value;
                OnPropertyChanged(string.Empty); // Уведомляем об изменении всех привязок
            }
        }
    }

    public string this[string key] =>
        _resourceManager.GetString(key, CurrentCulture) ?? $"[{key}]";

    public void SetCulture(string cultureName)
    {
        CurrentCulture = new CultureInfo(cultureName);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
