using GlobalBlueHomework.Features.Contracts;

namespace GlobalBlueHomework.Features.Services
{
    public interface IPurchaseValuesService
    {
        PurchaseValuesResponse Calculate(PurchaseValuesRequest request);
    }
}