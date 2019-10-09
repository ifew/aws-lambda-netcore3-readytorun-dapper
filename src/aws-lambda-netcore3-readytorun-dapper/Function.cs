using System.Linq;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Amazon.Lambda.APIGatewayEvents;
using System.Net;
using LambdaNative;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace aws_lambda_dapper
{
    public class Handler : IHandler<APIGatewayProxyRequest, APIGatewayProxyResponse>
    {
        public ILambdaSerializer Serializer => new Amazon.Lambda.Serialization.Json.JsonSerializer();

        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string unit_id = null;
            string lang = "THA";

            if (request.PathParameters != null && request.PathParameters.ContainsKey("unit_id")) {
                unit_id = request.PathParameters["unit_id"];
            }
            if(request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("lang")) {
                lang = request.QueryStringParameters["lang"];
            }

            using (MySqlConnection _connection = new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION")))  
            {  
                context.Logger.LogLine("_connection.ConnectionString: " + _connection.ConnectionString);
                context.Logger.LogLine("_connection.ToString: " + _connection.ToString());

                if (_connection.State == ConnectionState.Closed)  
                    _connection.Open();  
                
                context.Logger.LogLine("ServerVersion After Open: " + _connection.ServerVersion + "\nState: " + _connection.State.ToString());

                string sqlQuery = "SELECT * FROM test_member";
                List<Member> members = _connection.Query<Member>(sqlQuery).ToList();

                APIGatewayProxyResponse respond = new APIGatewayProxyResponse {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" },
                        { "X-Debug-UnitId", unit_id },
                        { "X-Debug-Lang", lang },
                    },
                    Body = JsonConvert.SerializeObject(members)
                };
                
                return respond;  
            } 
        }
        
    }
}
