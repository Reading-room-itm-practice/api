using System;

namespace Core.Common
{
    public static class LoggerMessages
    {
        public static string UnauthorizedOperation(Guid userId, string userName)
            => $"User{{ id: {userId}, name: {userName} }} - tried perform unauthorized action";
    }
}
