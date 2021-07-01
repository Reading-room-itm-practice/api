using System;

namespace WebAPI.Controllers
{
    internal class SwaggerOperationAttribute : Attribute
    {
        public string Summary { get; set; }
    }
}