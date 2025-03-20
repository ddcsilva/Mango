using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponAPIController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coupon>>> GetAllCoupons()
    {
        try
        {
            var coupons = await _context.Coupons.ToListAsync();
            return Ok(coupons);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Coupon>> GetCouponById(int id)
    {
        try
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

            if (coupon == null)
            {
                return NotFound($"Cupom com ID {id} não encontrado.");
            }

            return Ok(coupon);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor.");
        }
    }
}
