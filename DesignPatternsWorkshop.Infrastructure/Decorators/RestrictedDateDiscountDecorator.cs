using DesignPatternsWorkshop.Application.Strategies;

namespace DesignPatternsWorkshop.Infrastructure.Decorators;

public class RestrictedDateDiscountDecorator : IDiscountStrategy
{
    #region properties
    private IDiscountStrategy _strategy;
    private DateOnly _validDate;
    public string Name { get; }
    #endregion

    #region constructor
    public RestrictedDateDiscountDecorator(IDiscountStrategy strategy, DateOnly validDate)
    {
        _strategy = strategy;
        _validDate = validDate;
        Name = "Date specific discount";
    }
    #endregion

    #region methods
    public double ApplyDiscount(double amount)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        if(today.Month == _validDate.Month && today.Day == _validDate.Day)
        {
            return _strategy.ApplyDiscount(amount);
        }

        return amount;
    }
    #endregion
}
