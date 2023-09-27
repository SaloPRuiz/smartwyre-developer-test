using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Builders;

public class FixedCashAmountBuilder : IncentiveTypeBuilder
{
    public FixedCashAmountBuilder(IRebateDataStore rebateDataStore) : base(rebateDataStore)
    {
    }
    public override CalculateRebateResult Calculate(CalculateRebateResult rebateResult)
    {
        var rebate = rebateResult.Rebate;
        var product = rebateResult.Product;

        if (rebate == null)
        {
            rebateResult.Success = false;
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            rebateResult.Success = false;
        }
        else if (rebate.Amount == 0)
        {
            rebateResult.Success = false;
        }
        else
        {
            rebateResult.Success = true;
            rebateResult.RebateAmount = rebate.Amount;
        }

        return rebateResult;
    }
}