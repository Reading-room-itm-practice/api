﻿using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Middleware
{
    public class ResponseStatusCodeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJsonKeyValueGetter _jsonKeyValueGetter;
        private const string StatusCodeName = "statusCode";

        public ResponseStatusCodeMiddleware(RequestDelegate next, IJsonKeyValueGetter jsonKeyValueGetter)
        {
            _next = next;
            _jsonKeyValueGetter = jsonKeyValueGetter;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            try
            {
                await ChangeResponseStatusCode(context, originalBody);
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private async Task ChangeResponseStatusCode(HttpContext context, Stream originalBody)
        {
            using (var workStream = new MemoryStream())
            {
                string responseBody = await ReadResponseBodyFromStream(context, workStream);

                if (responseBody.Contains(StatusCodeName))
                {
                    string statusCode = _jsonKeyValueGetter.GetValueByKey(responseBody, StatusCodeName);
                    context.Response.StatusCode = int.Parse(statusCode);
                }

                await BackToOriginalBodyResponse(workStream, originalBody);
            }
        }

        private async Task<string> ReadResponseBodyFromStream(HttpContext context, Stream workStream)
        {
            context.Response.Body = workStream;
            await _next(context);
            context.Response.Body.Position = 0;
            var responseReader = new StreamReader(context.Response.Body);

            return responseReader.ReadToEnd();
        }

        private async Task BackToOriginalBodyResponse(Stream workStream, Stream originalBody)
        {
            workStream.Position = 0;
            await workStream.CopyToAsync(originalBody);
        }
    }
}
