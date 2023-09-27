using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IData;

public interface IProductDataStore
{
    public Product GetProduct(string productIdentifier);
}