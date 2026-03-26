namespace GlobalBlueHomework.Features.Contracts
{
    public record PurchaseValuesRequest(
        decimal? NetValue,
        decimal? GrossValue,
        decimal? VatValue,
        int? VatRate
    );
}