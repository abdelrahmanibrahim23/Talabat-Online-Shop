using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.Repository.Data
{
    public class storeContextSeed
    {
        public static async Task SeedAsync(StoryContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBreand.Any())
                {
                    var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBreand>>(brandData);
                    foreach (var brand in brands)
                        context.Set<ProductBreand>().Add(brand);
                }
                if (!context.ProductTypes.Any())
                {
                    var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/Types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                    foreach (var type in types)
                        context.Set<ProductType>().Add(type);

                }
                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    foreach (var product in products)
                        context.Set<Product>().Add(product);
                }
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                    var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    foreach (var deliveryMethod in Delivery)
                        context.Set<DeliveryMethod>().Add(deliveryMethod);
                }
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<storeContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
