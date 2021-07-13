using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserSearchDto : IResponseDto, ISearchable
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
