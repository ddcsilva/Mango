using AutoMapper;
using Mango.Services.CouponAPI.Application.DTOs;
using Mango.Services.CouponAPI.Domain.Models;

namespace Mango.Services.CouponAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponDTO>().ReverseMap();
    }
}
