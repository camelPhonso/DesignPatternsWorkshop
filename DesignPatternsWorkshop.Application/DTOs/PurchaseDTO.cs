using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Domain.Strategies;

namespace DesignPatternsWorkshop.Application.DTOs;

public record PurchaseDTO
{
    #region properties
    public int Id { get; set; }
    public List<ProductDTO> Products { get; set; }
    public IDiscountStrategy Discount { get; set; } = new NoDiscountStrategy();
    public string ErrorMessage { get; set; } = string.Empty;
    #endregion

    #region constructor
    public PurchaseDTO(int id, List<ProductDTO> products)
    {
        Id = id;
        Products = products;
    }
    #endregion

    #region methods
    /// <summary>
    /// Applies the given Discount Strategy to the Purchase total.
    /// </summary>
    /// <param name="discount"></param>
    public void SetDiscountStrategy(IDiscountStrategy discount)
    {
        Discount = discount;
    }

    /// <summary>
    /// Returns the sum of all Product prices with any active Discount Strategy applied.
    /// </summary>
    /// <returns></returns>
    public double GetTotal()
    {
        var baseTotal = Products.Sum(p => p.Price);
        return Discount.ApplyDiscount(baseTotal);
    }

    #endregion
}
