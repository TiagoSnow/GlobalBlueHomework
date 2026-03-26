using GlobalBlueHomework.Features.Contracts;

namespace GlobalBlueHomework.Features.Validation
{
    public class PurchaseValuesValidator : IPurchaseValuesValidator
    {
        private static readonly int[] ValidVatRates = [10, 13, 20];

        public Dictionary<string, string[]> Validate(PurchaseValuesRequest request)
        {
            var errors = new Dictionary<string, List<string>>();

            var providedAmounts = new[]
            {
                request.NetValue,
                request.GrossValue,
                request.VatValue
            }.Count(x => x.HasValue);

            if (providedAmounts == 0) AddError(errors, "Values", "One value must be provided: NetValue, GrossValue, or VatValue.");

            if (providedAmounts > 1) AddError(errors, "Values", "Only one value can be provided: NetValue, GrossValue, or VatValue.");
            
            if (request.NetValue.HasValue && request.NetValue.Value <= 0) AddError(errors, nameof(request.NetValue), "NetValue must be greater than 0.");
            
            if (request.GrossValue.HasValue && request.GrossValue.Value <= 0)AddError(errors, nameof(request.GrossValue), "GrossValue must be greater than 0.");

            if (request.VatValue.HasValue && request.VatValue.Value <= 0) AddError(errors, nameof(request.VatValue), "VatValue must be greater than 0.");
            
            if (!request.VatRate.HasValue) AddError(errors, nameof(request.VatRate), "VatRate is required.");
            
            else if (!ValidVatRates.Contains(request.VatRate.Value))
            {
                AddError(errors, nameof(request.VatRate), "VatRate must be 10, 13, or 20.");
            }

            return errors.ToDictionary(
                x => x.Key,
                x => x.Value.ToArray());
        }

        private static void AddError(
            Dictionary<string, List<string>> errors,
            string key,
            string message)
        {
            if (!errors.TryGetValue(key, out List<string>? value))
            {
                value = [];
                errors[key] = value;
            }

            value.Add(message);
        }
    }
}
