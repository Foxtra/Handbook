namespace Handbook.Localization;

/// <summary>
/// Интерфейс для переключения локализации
/// </summary>
internal interface ILocalizationManager
{
    /// <summary>
    /// Установить русскую локализацию
    /// </summary>
    void SetRussianCulture();

    /// <summary>
    /// Установить английскую локализацию
    /// </summary>
    void SetEnglishCulture();
}
