using DesignPatternsWorkshop.Application.Commands;
using DesignPatternsWorkshop.Application.DTOs;
using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Domain.Models;
using DesignPatternsWorkshop.Infrastructure.Commands;

namespace DesignPatternsWorkshop.Infrastructure.Services;

public class PurchaseService
{
    #region properties
    private readonly PurchaseDTO _purchase;
    private readonly PurchaseInvoker _invoker;
    #endregion

    #region constructor
    public PurchaseService()
    {
        _purchase = new PurchaseDTO(new Random().Next(), new List<ProductDTO>());
        _invoker = new PurchaseInvoker();
    }
    #endregion

    #region methods
    public PurchaseDTO GetPurchase()
    {
        List<ProductDTO> productsList = new();
        _purchase.Products.ForEach(p =>
        {
            var dto = new ProductDTO(p.Id, p.Name, p.Price, p.Quantity);
            productsList.Add(dto);
        });

        var purchaseDto = _purchase;
        return purchaseDto;
    }

    public void AddProduct(ProductDTO product)
    {
        var command = new AddProductCommand(_purchase, product);
        _invoker.ExecuteCommand(command);
    }

    public void RemoveProduct(ProductDTO product)
    {
        var command = new RemoveProductCommand(_purchase, product);
        _invoker.ExecuteCommand(command);
    }

    public void UndoLastAction()
    {
        _invoker.Undo();
    }

    public void RedoLastAction()
    {
        _invoker.Redo();
    }

    public void ApplyDiscount(IPurchaseCommand command)
    {
        _invoker.ExecuteCommand(command);
    }

    public double CalculateTotal()
    {
        var total = _purchase.GetTotal();
        return total;
    }

    public string GetDiscountStrategyName()
    {
        var discountName = _purchase.Discount.Name;
        return discountName;
    }
    #endregion
}
