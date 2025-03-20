using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponAPIController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private ResponseDTO _response = new();

    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetAllCoupons()
    {
        try
        {
            var coupons = await _context.Coupons.ToListAsync();
            _response.Result = coupons;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseDTO>> GetCouponById(int id)
    {
        try
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

            if (coupon == null)
            {
                _response.IsSuccess = false;
                _response.Message = $"Cupom com ID {id} não encontrado.";
                return NotFound(_response);
            }

            _response.Result = coupon;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
