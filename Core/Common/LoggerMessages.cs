using System;

namespace Core.Common
{
    public static class LoggerMessages
    {
        public static string UnauthorizeOperation(Guid userId, string userName)
            => $"User{{ id: {userId}, name: {userName} }} - tried perform unauthorize action";
    }
}
