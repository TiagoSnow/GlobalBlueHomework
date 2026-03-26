using GlobalBlueHomework.Features.Contracts;

namespace GlobalBlueHomework.Features.Validation
{
    public interface IPurchaseValuesValidator
    {
        Dictionary<string, string[]> Validate(PurchaseValuesRequest request);
    }
}
