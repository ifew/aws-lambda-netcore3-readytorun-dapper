using System.Linq;
using Amazon.Lambda.Core;
using LambdaNative;
using Amazon.Lambda.Serialization.Json;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace aws_lambda_dapper
{
    public class Handler : IHandler<string, List<Member>>
    {

        public ILambdaSerializer Serializer => new JsonSerializer();

        public List<Member> Handle(string request, ILambdaContext context)
        {
            using (MySqlConnection _connection = new MySqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION")))  
            {  
                context.Logger.LogLine("_connection.ConnectionString: " + _connection.ConnectionString);
                context.Logger.LogLine("_connection.ToString: " + _connection.ToString());

                if (_connection.State == ConnectionState.Closed)  
                    _connection.Open();  
                
                context.Logger.LogLine("ServerVersion After Open: " + _connection.ServerVersion + "\nState: " + _connection.State.ToString());

                string sqlQuery = "SELECT * FROM test_member";
                List<Member> members = _connection.Query<Member>(sqlQuery).ToList();
                
                return members;  
            } 
        }
        
    }
}
