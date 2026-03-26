using GlobalBlueHomework.Features.Contracts;

namespace GlobalBlueHomework.Features.Services;

public class PurchaseValuesService : IPurchaseValuesService
{
    public PurchaseValuesResponse Calculate(PurchaseValuesRequest request)
    {
        if (request.VatRate is null)
            throw new ArgumentException("VatRate is required.", nameof(request));

        var vatRate = request.VatRate.Value;

        if (request.NetValue is decimal netValue)
            return CalculateFromNetValue(netValue, vatRate);

        if (request.GrossValue is decimal grossValue)
            return CalculateFromGrossValue(grossValue, vatRate);

        if (request.VatValue is decimal vatValue)
            return CalculateFromVatValue(vatValue, vatRate);

        throw new ArgumentException( "invalid values provided", nameof(request));
    }

    private static PurchaseValuesResponse CalculateFromNetValue(decimal netValue, int vatRate)
    {
        var vatValue = netValue * vatRate / 100m;
        var grossValue = netValue + vatValue;

        return new PurchaseValuesResponse(
            NetValue: netValue,
            GrossValue: grossValue,
            VatValue: vatValue,
            VatRate: vatRate
        );
    }

    private static PurchaseValuesResponse CalculateFromGrossValue(decimal grossValue, int vatRate)
    {
        var netValue = grossValue / (1 + vatRate / 100m);
        var vatValue = grossValue - netValue;

        return new PurchaseValuesResponse(
            NetValue: netValue,
            GrossValue: grossValue,
            VatValue: vatValue,
            VatRate: vatRate
        );
    }

    private static PurchaseValuesResponse CalculateFromVatValue(decimal vatValue, int vatRate)
    {
        var netValue = vatValue / (vatRate / 100m);
        var grossValue = netValue + vatValue;

        return new PurchaseValuesResponse(
            NetValue: netValue,
            GrossValue: grossValue,
            VatValue: vatValue,
            VatRate: vatRate
        );
    }


}