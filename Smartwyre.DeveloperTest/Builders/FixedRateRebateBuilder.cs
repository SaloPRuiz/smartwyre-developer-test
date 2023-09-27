using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Builders;

public class FixedRateRebateBuilder : IncentiveTypeBuilder
{
    public FixedRateRebateBuilder(IRebateDataStore rebateDataStore) : base(rebateDataStore)
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
        else if (product == null)
        {
            rebateResult.Success = false;
        }
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        {
            rebateResult.Success = false;
        }
        else if (rebate.Percentage == 0 || product.Price == 0 || rebateResult.Volume == 0)
        {
            rebateResult.Success = false;
        }
        else
        {
            rebateResult.RebateAmount += product.Price * rebate.Percentage * rebateResult.Volume;
            rebateResult.Success = true;
        }

        return rebateResult;
    }
}