using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public APIGatewayProxyResponse Handler(APIGatewayProxyRequest input, ILambdaContext context)
    {

        try
        {
            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.SAEast1 // Define a região de São Paulo (sa-east-1)
            };
            var dynamodbContext = new DynamoDBContext(new AmazonDynamoDBClient(config));

            var json = JsonConvert.DeserializeObject<Produtos>(input.Body);

            var produto = dynamodbContext.LoadAsync<Produtos>(json?.Pk, json?.Sk).Result;

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(produto)
            };
        }
        catch (Exception ex)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "Message: " + ex.Message
            };
        }
    }
}
