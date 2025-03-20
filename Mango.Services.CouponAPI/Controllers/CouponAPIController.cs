using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponAPIController(AppDbContext context, IMapper mapper) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private ResponseDTO _response = new();

    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetAllCoupons()
    {
        try
        {
            var objList = await _context.Coupons.ToListAsync();
            _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);
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
            var obj = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

            if (obj == null)
            {
                _response.IsSuccess = false;
                _response.Message = $"Cupom com ID {id} não encontrado.";
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<CouponDTO>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
