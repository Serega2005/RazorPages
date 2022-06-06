namespace WebApplication4.Models;

public interface ICatalog
{
    List<Product> GetProducts();
    void AddProduct(Product product);
}