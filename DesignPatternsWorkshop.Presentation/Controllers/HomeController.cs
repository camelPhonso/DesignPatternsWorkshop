using DesignPatternsWorkshop.Application.DTOs;
using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Domain.Models;
using DesignPatternsWorkshop.Infrastructure.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatternsWorkshop.Infrastructure.Controllers;

public class HomeController : Controller
{
    #region properties
    private PurchaseService _service;
    #endregion

    #region constructor
    public HomeController(PurchaseService service)
    {
        _service = service;
    }
    #endregion

    #region endpoints
    public IActionResult Index()
    {
        try
        {
            PurchaseDTO purchase = _service.GetPurchase();
            return View(purchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public IActionResult GetProductList()
    {
        try
        {
            PurchaseDTO purchase = _service.GetPurchase();
            return PartialView("_ProductList", purchase.Products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public IActionResult GetTotal()
    {
        try
        {
            double total = _service.CalculateTotal();
            string discountName = _service.GetDiscountStrategyName();
            var TotalDisplay = new PurchaseTotalDTO(discountName, total);

            return PartialView("_PurchaseTotal", TotalDisplay);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public IActionResult ShowError(string error)
    {
        try
        {
            return PartialView("_ErrorMessage", new PurchaseErrorMessageDTO(error));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
}
