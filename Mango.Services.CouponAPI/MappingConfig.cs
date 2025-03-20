using AutoMapper;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDTO, Coupon>().ReverseMap();
        });

        return mappingConfig;
    }
}
