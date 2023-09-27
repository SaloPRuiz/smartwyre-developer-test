using System;
using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        try
        {
            // Test - Rebate Objects
            switch (rebateIdentifier)
            {
                case "1":
                    return new Rebate
                    {
                        Percentage = 5,
                        Incentive = IncentiveType.FixedRateRebate,
                    };

                case "2":
                    return new Rebate
                    {
                        Amount = 300,
                        Incentive = IncentiveType.FixedCashAmount
                    };

                case "3":
                    return new Rebate
                    {
                        Amount = 500,
                        Incentive = IncentiveType.AmountPerUom,
                    };

                default:
                    return new Rebate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        try
        {
           
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}