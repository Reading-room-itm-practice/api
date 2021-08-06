using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common;

namespace Core.DTOs
{
    public abstract class LikeableDto : IDto
    {
        public int LikesCount { get; set; }
        public bool LoggedUserLiked { get; set; }
    }
}
