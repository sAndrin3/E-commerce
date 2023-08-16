using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Product product = new Product();

        await ConsoleInterface.RunAsync(product);
    }
}
