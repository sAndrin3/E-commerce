using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Product : IProduct
{
    private readonly string _baseUrl = "http://localhost:3000";
    private readonly string _productEndpoint = "/product";
    private HttpClient _httpClient;

    public Product()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl + _productEndpoint);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(json);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}{_productEndpoint}/{id}");
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ProductDTO>(json);
    }

    public async Task AddProductAsync(ProductDTO product)
    {
        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(_baseUrl + _productEndpoint, content);
        System.Console.WriteLine(content);
        System.Console.WriteLine(json);
    }
        
    public async Task UpdateProductAsync(int id, ProductDTO product)
    {
        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        await _httpClient.PutAsync($"{_baseUrl}{_productEndpoint}/{id}", content);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _httpClient.DeleteAsync($"{_baseUrl}{_productEndpoint}/{id}");
    }
}
