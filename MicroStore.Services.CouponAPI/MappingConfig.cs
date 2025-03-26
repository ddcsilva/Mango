using AutoMapper;
using MicroStore.Services.CouponAPI.Application.DTOs;
using MicroStore.Services.CouponAPI.Domain.Models;

namespace MicroStore.Services.CouponAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponDTO>().ReverseMap();
    }
}
