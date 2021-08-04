using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    public class ReadStatusMapperProfile : Profile
    {
        public ReadStatusMapperProfile()
        {
            CreateMap<ReadStatus, ReadStatusDto>().ReverseMap();
            CreateMap<ReadStatusRequest, ReadStatus>().ReverseMap();
        }
    }
}
