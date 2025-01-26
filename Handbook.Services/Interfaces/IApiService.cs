using Handbook.Services.DTO;

namespace Handbook.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса загрузки данных через обращение к стороннему API
/// </summary>
public interface IApiService
{
    /// <summary>
    /// Загрузить список цилиндров
    /// </summary>
    public Task<IReadOnlyCollection<CylinderDto>> LoadCylinders();

    /// <summary>
    /// Загрузить список типов устройств
    /// </summary>
    public Task<IReadOnlyCollection<DeviceTypeDto>> LoadDeviceTypes();

    /// <summary>
    /// Загрузить список насосов
    /// </summary>
    public Task<IReadOnlyCollection<PumpDto>> LoadPumps();

    /// <summary>
    /// Загрузить список клапанов
    /// </summary>
    public Task<IReadOnlyCollection<ValveDto>> LoadValves();
}
