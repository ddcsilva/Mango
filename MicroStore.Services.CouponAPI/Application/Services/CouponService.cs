using AutoMapper;
using MicroStore.Services.CouponAPI.Application.DTOs;
using MicroStore.Services.CouponAPI.Application.Interfaces;
using MicroStore.Services.CouponAPI.Domain.Models;

namespace MicroStore.Services.CouponAPI.Application.Services;

public class CouponService(ICouponRepository couponRepository, IMapper mapper) : ICouponService
{
    public async Task<ResponseDTO> GetAllCouponsAsync()
    {
        var coupons = await couponRepository.GetAllAsync();
        var couponDTOs = mapper.Map<IEnumerable<CouponDTO>>(coupons);

        return new ResponseDTO { Result = couponDTOs };
    }

    public async Task<ResponseDTO> GetCouponByIdAsync(int id)
    {
        var coupon = await couponRepository.GetByIdAsync(id);
        if (coupon == null) return new ResponseDTO { IsSuccess = false, Message = "Cupom não encontrado" };

        return new ResponseDTO { Result = mapper.Map<CouponDTO>(coupon) };
    }

    public async Task<ResponseDTO> GetCouponByCodeAsync(string code)
    {
        var coupon = await couponRepository.GetByCodeAsync(code);
        if (coupon == null) return new ResponseDTO { IsSuccess = false, Message = "Cupom não encontrado" };

        return new ResponseDTO { Result = mapper.Map<CouponDTO>(coupon) };
    }

    public async Task<ResponseDTO> CreateCouponAsync(CouponDTO couponDTO)
    {
        if (couponDTO == null) return new ResponseDTO { IsSuccess = false, Message = "Dados inválidos." };

        var coupon = mapper.Map<Coupon>(couponDTO);
        await couponRepository.AddAsync(coupon);

        return new ResponseDTO { Result = mapper.Map<CouponDTO>(coupon) };
    }

    public async Task<ResponseDTO> UpdateCouponAsync(CouponDTO couponDTO)
    {
        if (couponDTO == null) return new ResponseDTO { IsSuccess = false, Message = "Dados inválidos." };

        var coupon = mapper.Map<Coupon>(couponDTO);
        await couponRepository.UpdateAsync(coupon);

        return new ResponseDTO { Result = mapper.Map<CouponDTO>(coupon) };
    }

    public async Task<ResponseDTO> DeleteCouponAsync(int id)
    {
        var coupon = await couponRepository.GetByIdAsync(id);
        if (coupon == null) return new ResponseDTO { IsSuccess = false, Message = "Cupom não encontrado" };

        await couponRepository.DeleteAsync(coupon);
        return new ResponseDTO { Message = "Cupom excluído com sucesso" };
    }
}

