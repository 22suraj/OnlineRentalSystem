using MongoDB.Driver;
using Microsoft.Extensions.Options;
using OnlineRentalSystemAPI.Model;

namespace OnlineRentalSystemAPI.Data
{
    public class ProductService
    {

        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _products = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Product>("Products");
        }


        public async Task<List<Product>> Get() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task Create(Product newProduct) =>
            await _products.InsertOneAsync(newProduct);
    }
}
