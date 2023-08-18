using System;
using System.Threading.Tasks;

public static class ConsoleInterface
{
    public static async Task RunAsync(Product product)
    {
        string message = $@"
        
▒█░░▒█ █▀▀ █░░ █▀▀ █▀▀█ █▀▄▀█ █▀▀ 
▒█▒█▒█ █▀▀ █░░ █░░ █░░█ █░▀░█ █▀▀ 
▒█▄▀▄█ ▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀▀ ▀░░░▀ ▀▀▀
        ";
        while (true)
        {
            Console.WriteLine(message);
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. View Product by ID");
            Console.WriteLine("3. Add Product");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("6. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ViewProductsAsync(product);
                    break;
                case "2":
                    await ViewProductByIdAsync(product);
                    break;
                case "3":
                    await AddProductAsync(product);
                    break;
                case "4":
                    await UpdateProductAsync(product);
                    break;
                case "5":
                    await DeleteProductAsync(product);
                    break;
                case "6":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
                
            }
        }
    }

    static async Task ViewProductsAsync(Product product)
    {
        var products = await product.GetAllProductsAsync();
        foreach (var productItem in products)
        {
            Console.WriteLine($" Name: {productItem.Name}, category: {productItem.Category} Price: {productItem.Price}");
        }
    }

    static async Task AddProductAsync(Product product)
{
    Console.WriteLine("Enter product name:");
    string name = Console.ReadLine();

    Console.WriteLine("Enter product description:");
    string description = Console.ReadLine();

    Console.WriteLine("Enter product category (MountainBikes, RoadBikes, BMXBikes, EBikes):");
    string categoryInput = Console.ReadLine();

    if (Enum.TryParse<ProductCategory>(categoryInput, out ProductCategory category))
    {
        Console.WriteLine("Enter product price:");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            ProductDTO newProduct = new ProductDTO
            {
                Name = name,
                Description = description,
                Price = price,
                Category = category
            };

            await product.AddProductAsync(newProduct);
            Console.WriteLine(newProduct);
            Console.WriteLine("Product added successfully!");
        }
        else
        {
            Console.WriteLine("Invalid price input. Product not added.");
        }
    }
    else
    {
        Console.WriteLine("Invalid category input. Product not added.");
    }
}


 static async Task UpdateProductAsync(Product product)
{
    var products = await product.GetAllProductsAsync();

    Console.WriteLine("Select a product to update:");
    foreach (var productItem in products)
    {
        Console.WriteLine($"ID: {productItem.id}, Name: {productItem.Name}");
    }

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var existingProduct = await product.GetProductByIdAsync(id);
        if (existingProduct != null)
        {
            Console.WriteLine("Enter updated product name:");
            string name = Console.ReadLine();
            existingProduct.Name = name;

            Console.WriteLine("Enter updated product description:");
            string description = Console.ReadLine();
            existingProduct.Description = description;

            Console.WriteLine("Enter updated product category (MountainBikes, RoadBikes, BMXBikes, EBikes):");
            string categoryInput = Console.ReadLine();
            if (Enum.TryParse<ProductCategory>(categoryInput, out ProductCategory category))
            {
                existingProduct.Category = category;
            }
            else
            {
                Console.WriteLine("Invalid category input. Category not updated.");
            }

            Console.WriteLine("Enter updated product price:");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                existingProduct.Price = price;
            }
            else
            {
                Console.WriteLine("Invalid price input. Price not updated.");
            }

            await product.UpdateProductAsync(id, existingProduct);
            Console.WriteLine("Product updated successfully!");
        }
        else
        {
            Console.WriteLine($"Product with ID {id} not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid ID input.");
    }
}

    static async Task DeleteProductAsync(Product product)
    {
        Console.WriteLine("Enter the ID of the product to delete:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var existingProduct = await product.GetProductByIdAsync(id);
            if (existingProduct != null)
            {
                await product.DeleteProductAsync(id);
                Console.WriteLine("Product deleted successfully!");
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID input.");
        }
    }
      static async Task ViewProductByIdAsync(Product product)
    {
        Console.WriteLine("Enter the ID of the product to view:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var productItem = await product.GetProductByIdAsync(id);
            if (productItem != null)
            {
                Console.WriteLine($" Name: {productItem.Name}, Category: {productItem.Category}, Price: {productItem.Price}, Description: {productItem.Description}");
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID input.");
        }
    }
}
