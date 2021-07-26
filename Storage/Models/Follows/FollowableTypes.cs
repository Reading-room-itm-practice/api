using System;

namespace Storage.Models.Follows
{
    public enum FollowableTypes
    {
        Category = 1,
        Author = 2,
        User = 3
    }

    public static class FollowableTypesExtension
    {
        public static string ToStringValue(this FollowableTypes type)
        {
            return type switch
            {
                FollowableTypes.Category => "Category",
                FollowableTypes.Author => "Author",
                FollowableTypes.User => "User",
                _ => throw new Exception("Followable type not found"),
            };
        }
    }
}
