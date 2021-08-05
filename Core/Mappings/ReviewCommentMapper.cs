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
    class ReviewCommentMapper : Profile
    {
        public ReviewCommentMapper()
        {
            CreateMap<ReviewCommentRequest, ReviewComment>().ReverseMap();
            CreateMap<ReviewComment, ReviewCommentDto>().ReverseMap();
        }
    }
}
