using Mango.Web.Features.Coupons.DTOs;
using Mango.Web.Features.Coupons.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Features.Coupons.Controllers;

public class CouponController(ICouponService couponService) : Controller
{
    public async Task<IActionResult> Index()
    {
        List<CouponDTO> couponList = [];

        var response = await couponService.GetAllCouponsAsync();

        if (response.IsSuccess && response.Result is not null)
        {
            couponList = JsonConvert.DeserializeObject<List<CouponDTO>>(response.Result.ToString()!) ?? [];
        }

        return View(couponList);
    }
}
