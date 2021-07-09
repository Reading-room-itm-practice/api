using Core.Exceptions;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            using (var ms = new MemoryStream())
            {
                context.Response.Body = ms;
                await _next(context);

                context.Response.Body.Position = 0;

                var responseReader = new StreamReader(context.Response.Body);

                var responseContent = responseReader.ReadToEnd();

                if(responseContent.Contains("statusCode"))
                {
                    context.Response.StatusCode = GetStatusCode(responseContent);
                }
            }

            context.Response.Body = originalBody;
        }


        private int GetStatusCode(string responseContent)
        {
            var data = (JObject)JsonConvert.DeserializeObject(responseContent);
            string statusCode = data["statusCode"].Value<string>();

            return int.Parse(statusCode);
        }
    }
}
