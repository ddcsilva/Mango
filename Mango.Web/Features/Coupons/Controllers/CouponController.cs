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
    public async Task<IActionResult> Create(CouponDTO couponDTO)
    {
        if (!ModelState.IsValid) return View(couponDTO);

        var response = await couponService.CreateCouponAsync(couponDTO);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Cupom criado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = response?.Message ?? "Erro ao criar cupom.";
        return View(couponDTO);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int couponId)
    {
        if (couponId <= 0)
        {
            TempData["error"] = "ID inválido.";
            return RedirectToAction(nameof(Index));
        }

        var response = await couponService.GetCouponByIdAsync(couponId);

        if (response is null || !response.IsSuccess || response.Result is null)
        {
            TempData["error"] = "Cupom não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        var json = response.Result.ToString();

        if (string.IsNullOrWhiteSpace(json))
        {
            TempData["error"] = "Resposta inválida da API.";
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
            TempData["success"] = "Cupom excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = response?.Message ?? "Erro ao excluir cupom.";
        return View(model);
    }
}
