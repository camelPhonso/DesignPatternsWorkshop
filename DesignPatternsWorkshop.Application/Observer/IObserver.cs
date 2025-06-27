using DesignPatternsWorkshop.Application.DTOs;

namespace DesignPatternsWorkshop.Application.Observer;

public interface IObserver
{
    void Update(PurchaseDTO purchase);
}
