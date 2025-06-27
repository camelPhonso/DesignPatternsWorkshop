using System.Net.Mail;
using DesignPatternsWorkshop.Application.Observer;
using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Domain.Strategies;

namespace DesignPatternsWorkshop.Application.DTOs;

public record PurchaseDTO : ISubject
{
    #region properties
    public int Id { get; set; }
    public List<ProductDTO> Products { get; set; }
    public IDiscountStrategy Discount { get; set; } = new NoDiscountStrategy();
    private List<IObserver> _observers = new();
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

    public void Attach(IObserver observer) => _observers.Add(observer);

    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify() => _observers.ForEach(observer => observer.Update(this));

    #endregion
}
