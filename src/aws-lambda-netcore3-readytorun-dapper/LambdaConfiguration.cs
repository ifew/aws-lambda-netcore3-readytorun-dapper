using System.IO;
using Microsoft.Extensions.Configuration;

namespace aws_lambda_dapper
{
    public static class LambdaConfiguration
    {
        private static IConfigurationRoot instance = null;
        public static IConfigurationRoot Instance
        {
            get 
            {
                return instance ?? (instance = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddEnvironmentVariables()
                    .Build());
            }
        }
    }
}