using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Interfaces.Authors
{
    public interface IAuthorUpdater
    {
        public Task UpdateAuthor(UpdateAuthorDto updateModel, int id);
    }
}
