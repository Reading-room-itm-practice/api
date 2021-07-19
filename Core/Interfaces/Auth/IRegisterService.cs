﻿using Core.DTOs;
using Core.Requests;
using Core.ServiceResponses;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IRegisterService
    {
        public Task<ServiceResponse> Register(RegisterRequest model);
        public Task<ServiceResponse> ConfirmEmail(EmailDto model);
    }
}
