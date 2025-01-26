using AutoMapper;
using Handbook.Models;
using Handbook.Services.DTO;

namespace Handbook.Mapping;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DeviceTypeDto, DeviceType>();
        CreateMap<CylinderDto, Cylinder>();
        CreateMap<PumpDto, Pump>();
        CreateMap<ValveDto, Valve>();
    }
}
