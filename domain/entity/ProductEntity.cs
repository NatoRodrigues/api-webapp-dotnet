using System;
using Amazon.DynamoDBv2.DataModel;

namespace webapp.entity
{
    [DynamoDBTable("Products")]
    public class ProductEntity
    {
        [DynamoDBHashKey("ProductId")]
        public string ProductId { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty("Description")]
        public string Description { get; set; } = string.Empty;

        [DynamoDBProperty("Price")]
        public decimal Price { get; set; }

        [DynamoDBProperty("Quantity")]
        public int Quantity { get; set; }

        [DynamoDBProperty("Category")]
        public string Category { get; set; } = string.Empty;

        [DynamoDBProperty("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DynamoDBProperty("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
