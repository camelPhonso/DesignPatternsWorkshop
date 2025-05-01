using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Infrastructure.Strategies;
using DesignPatternsWorkshop.Domain.Result;

namespace DesignPatternsWorkshop.Infrastructure.Factories;

public class DiscountStrategyFactory
{
    public Result<IDiscountStrategy, string> CreateDiscountStrategy(string discountStrategyName, double value)
    {
        switch (discountStrategyName)
        {
            case "percentage":
                return new PercentageDiscountStrategy(value);
            case "fixed":
                return new FixedDiscountStrategy(value);
            case "bundle":
                return new BundleDiscountStrategy(value);
            default:
                return "Invalid discount strategy identifier";
        }
    }
}
