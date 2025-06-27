namespace DesignPatternsWorkshop.Application.Observer;

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver obsserver);
    void Notify();
}
