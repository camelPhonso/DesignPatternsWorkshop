using DesignPatternsWorkshop.Application.Commands;
using DesignPatternsWorkshop.Application.DTOs;
using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Infrastructure.Factories;
using DesignPatternsWorkshop.Infrastructure.Services;
using DesignPatternsWorkshop.Infrastructure.Strategies;
using Microsoft.AspNetCore.SignalR;

namespace DesignPatternsWorkshop.Presentation.Hubs;

public class PurchaseHub : Hub
{
    private PurchaseService _service;
    private DiscountStrategyFactory _factory;

    public PurchaseHub(PurchaseService service, DiscountStrategyFactory factory)
    {
        _service = service;
        _factory = factory;
    }

    public async Task AddProduct(ProductDTO product)
    {
        _service.AddProduct(product);

        await Clients.All.SendAsync("UpdatePurchase");
    }

    public async Task RemoveProduct(ProductDTO product)
    {
        _service.RemoveProduct(product);

        await Clients.All.SendAsync("UpdatePurchase");
    }

    public async Task Undo()
    {
        _service.UndoLastAction();

        await Clients.All.SendAsync("UpdatePurchase");
    }

    public async Task Redo()
    {
        _service.RedoLastAction();

        await Clients.All.SendAsync("UpdatePurchase");
    }

    public async Task AddDiscount(string discountType, double value)
    {
        var discountStrategy = _factory.CreateDiscountStrategy(discountType, value);
        var discountCommand = new AddDiscountCommand(_service.GetPurchase(), discountStrategy);
        _service.ApplyDiscount(discountCommand);
        await Clients.All.SendAsync("UpdatePurchase");
    }
}
