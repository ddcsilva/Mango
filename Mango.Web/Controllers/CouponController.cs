using Mango.Web.DTOs;
using Mango.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    public async Task<IActionResult> Index()
    {
        List<CouponDTO>? couponList = [];

        var response = await _couponService.GetAllCouponsAsync();

        if (response != null && response.IsSuccess)
        {
            couponList = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
        }

        return View(couponList);
    }
}
