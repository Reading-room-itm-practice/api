using Core.Common;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class ApproveFriendRequest : IRequest, IApproveable
    {
        public bool Approved { get; set; }
    }
}
