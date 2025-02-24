using DesignPatternsWorkshop.Application.DTOs;
using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Domain.Strategies;

namespace DesignPatternsWorkshop.Application.Commands;

public class AddDiscountCommand : IPurchaseCommand
{
    #region properties
    private PurchaseDTO _purchase;
    private IDiscountStrategy _discount;
    #endregion

    #region constructor
    public AddDiscountCommand(PurchaseDTO purchase, IDiscountStrategy discount)
    {
        _purchase = purchase;
        _discount = discount;
    }
    #endregion

    #region methods
    public void Execute()
    {
        _purchase.SetDiscountStrategy(_discount);
    }

    public void Revert()
    {
        var noDiscount = new NoDiscountStrategy();
        _purchase.SetDiscountStrategy(noDiscount);
    }
    #endregion
}
