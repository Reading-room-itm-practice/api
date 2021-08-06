using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Storage.Interfaces;

namespace Core.Services
{
    public class Helper : IHelper
    {
        private readonly ILoggedUserProvider _loggedUserProvider;

        public Helper(ILoggedUserProvider loggedUserProvider)
        {
            _loggedUserProvider = loggedUserProvider;
        }
        public Guid Get()
        {
            try
            {
                return _loggedUserProvider.GetUserId();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
