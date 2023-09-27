using Smartwyre.DeveloperTest.IData;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        // Test - Rebate Objects
        switch (productIdentifier)
        {
            case "1":
                return new Product
                {
                    Price = 200,
                    SupportedIncentives = SupportedIncentiveType.FixedRateRebate 
                };
                
            case "2":
                return new Product
                {
                    SupportedIncentives = SupportedIncentiveType.FixedCashAmount 
                };
            
            case "3":
                return new Product()
                {
                    SupportedIncentives = SupportedIncentiveType.AmountPerUom
                };
            
            default:
                return new Product();
        }
    }
}
