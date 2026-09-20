using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using WebApp.Domain.Entities;

namespace WebApp.Endpoints;

public static class ProductAPI
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "ok");

        app.MapGet("/api/products", async (IAmazonDynamoDB db) =>
        {
            var res = await db.ScanAsync(new ScanRequest { TableName = "webapp-table" });
            var products = res.Items.Select(i => new
            {
                id = i["id"].S,
                name = i.GetValueOrDefault("product_name")?.S,
                price = decimal.Parse(i.GetValueOrDefault("price")?.N ?? "0", System.Globalization.CultureInfo.InvariantCulture)
            });
            return Results.Ok(products);
        });

        app.MapPost("/api/products", async (ProductDto p, IAmazonDynamoDB db) =>
        {
            var id = Guid.NewGuid().ToString();
            
            // Usando sua entidade de domínio opcionalmente para validação
            var product = new Product(id, p.Name, p.Price);

            await db.PutItemAsync("webapp-table", new Dictionary<string, AttributeValue>
            {
                ["id"] = new AttributeValue { S = id },
                ["product_name"] = new AttributeValue { S = product.Name },
                ["price"] = new AttributeValue { N = product.Price.ToString(System.Globalization.CultureInfo.InvariantCulture) }
            });
            
            return Results.Created($"/api/products/{id}", new { id = product.Name, product.Price });
        });
    }
}

public record ProductDto(string Name, decimal Price);