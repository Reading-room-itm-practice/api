using Core.Common;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class SendFriendRequest : IRequest
    {
        [Required]
        public Guid ToId { get; set; }
    }
}
