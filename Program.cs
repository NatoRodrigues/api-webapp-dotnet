using Amazon.DynamoDBv2;
using Amazon.Runtime;
using WebApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAmazonDynamoDB>(_ =>
{
    // Quando a aplicação roda DENTRO da instância EC2 simulada,'localhost' apontaria para a própria VM. Usamos a variável de ambiente ou o gateway do LocalStack.
    var serviceUrl = Environment.GetEnvironmentVariable("AWS_SERVICE_URL") ?? "http://host.docker.internal:4566";

    var config = new AmazonDynamoDBConfig
    {
        ServiceURL = serviceUrl,
        AuthenticationRegion = "us-east-1"
    };
    
    return new AmazonDynamoDBClient(
        new BasicAWSCredentials("mock_access_key", "mock_secret_key"), config);
});

var app = builder.Build();

// Mapeia os endpoints organizados no arquivo ProductAPI.cs
app.MapProductEndpoints();

app.Run();