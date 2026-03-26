using GlobalBlueHomework.Features.Contracts;
using GlobalBlueHomework.Features.Services;
using Xunit;

namespace GlobalBlueHomework.UnitTests.Features.PurchaseValues;

public class PurchaseValuesServiceTests
{
    private readonly PurchaseValuesService _service = new();

    [Fact]
    public void Calculate_ShouldCalculateFromNetValue()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 100m,
            GrossValue: null,
            VatValue: null,
            VatRate: 20);

        var result = _service.Calculate(request);

        Assert.Equal(100m, result.NetValue);
        Assert.Equal(20m, result.VatValue);
        Assert.Equal(120m, result.GrossValue);
        Assert.Equal(20, result.VatRate);
    }

    [Fact]
    public void Calculate_ShouldCalculateFromGrossValue()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: 120m,
            VatValue: null,
            VatRate: 20);

        var result = _service.Calculate(request);

        Assert.Equal(100m, result.NetValue);
        Assert.Equal(20m, result.VatValue);
        Assert.Equal(120m, result.GrossValue);
        Assert.Equal(20, result.VatRate);
    }

    [Fact]
    public void Calculate_ShouldCalculateFromVatValue()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: null,
            VatValue: 20m,
            VatRate: 20);

        var result = _service.Calculate(request);

        Assert.Equal(100m, result.NetValue);
        Assert.Equal(20m, result.VatValue);
        Assert.Equal(120m, result.GrossValue);
        Assert.Equal(20, result.VatRate);
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenVatRateIsNull()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 100m,
            GrossValue: null,
            VatValue: null,
            VatRate: null);

        Assert.Throws<ArgumentException>(() => _service.Calculate(request));
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenNoAmountIsProvided()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: null,
            VatValue: null,
            VatRate: 20);

        Assert.Throws<ArgumentException>(() => _service.Calculate(request));
    }
}