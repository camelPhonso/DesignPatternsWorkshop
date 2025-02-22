﻿using DesignPatternsWorkshop.Application.Strategies;

namespace DesignPatternsWorkshop.Domain.Strategies;

/// <summary>
/// Represents the default discount strategy, which is no discount.
/// </summary>
/// <Returns>Original Base Price unchanged.</Returns>
public class NoDiscountStrategy : IDiscountStrategy
{
    public double ApplyDiscount(double price)
    {
        return price;
    }
}
