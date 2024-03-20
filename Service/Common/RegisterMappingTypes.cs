using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Common
{
    public static class RegisterMappingTypes
    {
        public static void RegisterMapsterMappingTypes(this IServiceCollection services)
        {
            // Profile for DeviceType to DeviceTypeResponse
            TypeAdapterConfig<DeviceType, DeviceTypeResponse>
                .NewConfig()
                .Map(dest => dest.NumberOfDevices, src => src.Devices.Count);


        }
    }
}
