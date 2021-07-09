using Core.Exceptions;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ResponseMiddleware 
    {
        private readonly RequestDelegate _next;
        private const string StatusCodeName = "statusCode";
        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;
                    await _next(context);
                    context.Response.Body.Position = 0;
                    var responseReader = new StreamReader(context.Response.Body);
                    var responseContent = responseReader.ReadToEnd();

                    if (responseContent.Contains(StatusCodeName))
                    {
                        context.Response.StatusCode = GetStatusCode(responseContent);
                    }

                    memoryStream.Position = 0;
                    await memoryStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private int GetStatusCode(string responseContent)
        {
            var data = (JObject)JsonConvert.DeserializeObject(responseContent);
            string statusCode = data[StatusCodeName].Value<string>();

            return int.Parse(statusCode);
        }
    }
}
