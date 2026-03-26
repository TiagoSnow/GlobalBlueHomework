namespace GlobalBlueHomework.Features.Contracts
{
    public record PurchaseValuesResponse(
        decimal NetValue,
        decimal GrossValue,
        decimal VatValue,
        int VatRate
    );
}