using Mango.Web.Features.Coupons.DTOs;
using Mango.Web.Features.Coupons.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Features.Coupons.Controllers;

public class CouponController(ICouponService couponService) : Controller
{
    [HttpGet]
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CouponDTO model)
    {
        if (!ModelState.IsValid) return View(model);

        var response = await couponService.CreateCouponAsync(model);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, response?.Message ?? "Erro desconhecido ao criar cupom.");

        return View(model);
    }
}
