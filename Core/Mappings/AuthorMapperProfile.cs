using AutoMapper;
using Core.DTOs;
using Core.Requests;
using Storage.Models;

namespace Core.Mappings
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorRequest, Author>().ReverseMap();
            CreateMap<Author, ApprovedAuthorDto>().ReverseMap();
            CreateMap<ApproveAuthorRequest, Author>().ReverseMap();
        }
    }
}
