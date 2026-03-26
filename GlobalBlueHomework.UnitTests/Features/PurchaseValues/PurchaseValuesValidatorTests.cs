using GlobalBlueHomework.Features.Contracts;
using GlobalBlueHomework.Features.Validation;
using Xunit;

namespace GlobalBlueHomework.UnitTests.Features.PurchaseValues;

public class PurchaseValuesValidatorTests
{
    private readonly PurchaseValuesValidator _validator = new();

    [Fact]
    public void Validate_ShouldReturnError_WhenNoAmountProvided()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: null,
            VatValue: null,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenMoreThanOneAmountProvided()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 100m,
            GrossValue: 120m,
            VatValue: null,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenVatRateIsMissing()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 100m,
            GrossValue: null,
            VatValue: null,
            VatRate: null);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
        Assert.Contains(nameof(request.VatRate), errors.Keys);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenVatRateIsInvalid()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 100m,
            GrossValue: null,
            VatValue: null,
            VatRate: 17);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
        Assert.Contains(nameof(request.VatRate), errors.Keys);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenNetValueIsZero()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 0m,
            GrossValue: null,
            VatValue: null,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
        Assert.Contains(nameof(request.NetValue), errors.Keys);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenGrossValueIsZero()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: 0m,
            VatValue: null,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
        Assert.Contains(nameof(request.GrossValue), errors.Keys);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenVatValueIsZero()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: null,
            VatValue: 0m,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.NotEmpty(errors);
        Assert.Contains(nameof(request.VatValue), errors.Keys);
    }

    [Fact]
    public void Validate_ShouldReturnNoErrors_WhenRequestIsValidWithNetValue()
    {
        var request = new PurchaseValuesRequest(
            NetValue: 100m,
            GrossValue: null,
            VatValue: null,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.Empty(errors);
    }

    [Fact]
    public void Validate_ShouldReturnNoErrors_WhenRequestIsValidWithGrossValue()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: 120m,
            VatValue: null,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.Empty(errors);
    }

    [Fact]
    public void Validate_ShouldReturnNoErrors_WhenRequestIsValidWithVatValue()
    {
        var request = new PurchaseValuesRequest(
            NetValue: null,
            GrossValue: null,
            VatValue: 20m,
            VatRate: 20);

        var errors = _validator.Validate(request);

        Assert.Empty(errors);
    }
}