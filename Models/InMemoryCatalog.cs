using System.Collections.Concurrent;

namespace WebApplication4.Models;

public class InMemoryCatalog : ICatalog
{
    private ConcurrentBag<Product> _products = new();

    public List<Product> GetProducts()
    {
        return _products.ToList();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
}