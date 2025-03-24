using Microsoft.AspNetCore.Mvc;
using Mango.Services.CouponAPI.Application.DTOs;
using Mango.Services.CouponAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
[Authorize]
public class CouponAPIController(ICouponService couponService) : ControllerBase
{
    private readonly ICouponService _couponService = couponService;

    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetAllCoupons()
    {
        var response = await _couponService.GetAllCouponsAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseDTO>> GetCouponById(int id)
    {
        var response = await _couponService.GetCouponByIdAsync(id);
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<ResponseDTO>> GetCouponByCode(string code)
    {
        var response = await _couponService.GetCouponByCodeAsync(code);
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ResponseDTO>> CreateCoupon([FromBody] CouponDTO couponDTO)
    {
        var response = await _couponService.CreateCouponAsync(couponDTO);
        return CreatedAtAction(nameof(GetCouponById), new { id = ((CouponDTO)response.Result!).CouponId }, response);
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ResponseDTO>> UpdateCoupon([FromBody] CouponDTO couponDTO)
    {
        var response = await _couponService.UpdateCouponAsync(couponDTO);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ResponseDTO>> DeleteCoupon(int id)
    {
        var response = await _couponService.DeleteCouponAsync(id);
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }
}
