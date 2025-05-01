using DesignPatternsWorkshop.Application.DTOs;

namespace DesignPatternsWorkshop.Application.Commands;

public record AddProductCommand : IPurchaseCommand
{
    #region properties
    private readonly PurchaseDTO _purchase;
    private readonly ProductDTO _product;
    #endregion

    #region constructor
    public AddProductCommand(PurchaseDTO purchase, ProductDTO product)
    {
        _purchase = purchase;
        _product = product;
    }
    #endregion

    #region methods
    public void Execute() => _purchase.Products.Add(_product);

    public void Revert() => _purchase.Products.Remove(_product);
    #endregion
}
