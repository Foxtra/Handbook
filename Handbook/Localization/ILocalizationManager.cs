namespace Handbook.Localization;

/// <summary>
/// Интерфейс для переключения локализации
/// </summary>
public interface ILocalizationManager
{
    /// <summary>
    /// Получение локализированной строки
    /// </summary>
    /// <param name="key">Ключ-идентификатор строки в файле ресурсов локализации</param>
    /// <returns></returns>
    string this[string key] { get; }

    /// <summary>
    /// Устанавливает текущую культуру (меняет локализацию)
    /// </summary>
    /// <param name="cultureName">
    /// Имя культуры для утановки ("en", "ru").
    /// Должно быть валидным значением <see cref="CultureInfo"/>.
    /// </param>
    void SetCulture(string cultureName);
}
