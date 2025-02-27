using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Infrastructure.Decorators;
using DesignPatternsWorkshop.Infrastructure.Strategies;

namespace DesignPatternsWorkshop.Infrastructure.Factories;

public class DiscountStrategyFactory
{
    public IDiscountStrategy CreateDiscountStrategy(string discountStrategyName, double value)
    {
        switch (discountStrategyName)
        {
            case "percentage":
                return new PercentageDiscountStrategy(value);
            case "fixed":
                return new FixedDiscountStrategy(value);
            case "bundle":
                return new BundleDiscountStrategy(value);
            case "birthday":
                var birthdayStrategy = new PercentageDiscountStrategy(value);
                // this would obviously be some form of validation logic
                // returning us the birth date of our user, Jane Austen
                var clientBirthday = new DateOnly(1775, 12, 16);
                return new RestrictedDateDiscountDecorator(birthdayStrategy, clientBirthday);
            case "workshop":
                var workshopStrategy = new PercentageDiscountStrategy(value);
                var workshopDate = new DateOnly(2025, 04, 10);
                return new RestrictedDateDiscountDecorator(workshopStrategy, workshopDate);
            default:
                throw new ArgumentException("Invalid discount strategy identifier");
        }
    }
}
