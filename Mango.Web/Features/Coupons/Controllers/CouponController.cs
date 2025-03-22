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
        var response = await couponService.GetAllCouponsAsync();

        if (response is null || !response.IsSuccess || response.Result is null)
        {
            return View(new List<CouponDTO>());
        }

        var json = response.Result.ToString();

        if (string.IsNullOrWhiteSpace(json))
        {
            return View(new List<CouponDTO>());
        }

        var coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(json) ?? [];

        return View(coupons);
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

        if (response is not null && response.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, response?.Message ?? "Erro desconhecido ao criar cupom.");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int couponId)
    {
        if (couponId <= 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var response = await couponService.GetCouponByIdAsync(couponId);

        if (response is null || !response.IsSuccess || response.Result is null)
        {
            return RedirectToAction(nameof(Index));
        }

        var json = response.Result.ToString();

        if (string.IsNullOrWhiteSpace(json))
        {
            return RedirectToAction(nameof(Index));
        }

        var coupon = JsonConvert.DeserializeObject<CouponDTO>(json);

        return View(coupon);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(CouponDTO model)
    {
        if (model.CouponId <= 0)
        {
            ModelState.AddModelError(string.Empty, "ID inválido para exclusão.");
            return View(model);
        }

        var response = await couponService.DeleteCouponAsync(model.CouponId);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
