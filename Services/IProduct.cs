using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProduct
{
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    Task AddProductAsync(ProductDTO product);
    Task UpdateProductAsync(int id, ProductDTO product);
    Task DeleteProductAsync(int id);
}
