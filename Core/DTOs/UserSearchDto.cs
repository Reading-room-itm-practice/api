using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserSearchDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
