using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Builders;

public class AmountPerUomBuilder : IncentiveTypeBuilder
{
    public AmountPerUomBuilder(IRebateDataStore rebateDataStore) : base(rebateDataStore)
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
        else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            rebateResult.Success = false;
        }
        else if (rebate.Amount == 0 || rebateResult.Volume == 0)
        {
            rebateResult.Success = false;
        }
        else
        {
            rebateResult.RebateAmount += rebate.Amount * rebateResult.Volume;
            rebateResult.Success = true;
        }

        return rebateResult;
    }
}