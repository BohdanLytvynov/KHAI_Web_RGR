using AutoMapper;
using BookStore.BLL.Dto.Author;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Mapping.Authors
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForSourceMember(x=>x.Book_Authors, conf=>conf.DoNotValidate())
                .ReverseMap();

            CreateMap<Author, CreateAuthorDto>()
                .ForSourceMember(x =>x.Book_Authors, conf => conf.DoNotValidate())
                .ForSourceMember(x => x.Id, conf => conf.DoNotValidate())
                .ReverseMap();
        }
    }
}
