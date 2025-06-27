using DesignPatternsWorkshop.Application.Commands;
using DesignPatternsWorkshop.Application.DTOs;
using DesignPatternsWorkshop.Application.Observer;
using DesignPatternsWorkshop.Application.Strategies;
using DesignPatternsWorkshop.Infrastructure.Factories;

namespace DesignPatternsWorkshop.Infrastructure.Commands;

public class PurchaseInvoker : IObserver
{
    #region properties
    private readonly Stack<IPurchaseCommand> _undoStack = new();
    private readonly Stack<IPurchaseCommand> _redoStack = new();
    #endregion

    #region methods
    /// <summary>
    /// Executes a given Purchase Command and clears the Redo Stack.
    /// </summary>
    /// <param name="command"></param>
    public void ExecuteCommand(IPurchaseCommand command)
    {
        command.Execute();
        _undoStack.Push(command);
        _redoStack.Clear();
    }

    /// <summary>
    /// Calls the Undo method of the last command in the Undo Stack and moves it to the Redo Stack.
    /// </summary>
    public void Undo()
    {
        if(_undoStack.Count == 0) return;

        var command = _undoStack.Pop();
        command.Revert();
        _redoStack.Push(command);
    }

    /// <summary>
    /// Executes the latest command in the Redo Stack and moves it to the Undo Stack.
    /// </summary>
    public void Redo()
    {
        if (_redoStack.Count == 0) return;

        var command = _redoStack.Pop();
        command.Execute();
        _undoStack.Push(command);
    }

    public void Update(PurchaseDTO purchase)
    {
        if(purchase.Products.Count % 5 == 0)
        {
             int predicate = purchase.Products.Count / 5;
            List<ProductDTO> products = purchase.Products.OrderBy(product => product.Price).Take(predicate).ToList();
            double totalDiscount = products.Sum(product => product.Price);

            var factory = new DiscountStrategyFactory();
            IDiscountStrategy itemDiscount = factory.CreateDiscountStrategy("fixed", totalDiscount);

            purchase.SetDiscountStrategy(itemDiscount);
        }
    }
    #endregion
}
