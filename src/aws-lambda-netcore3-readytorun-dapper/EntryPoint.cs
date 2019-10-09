using System.Collections.Generic;
using aws_lambda_dapper;

namespace LambdaNative
{
    public static class EntryPoint
    {
        public static void Main()
        {
            LambdaNative.Run<Handler, string, List<Member>>();
        }
    }
}
