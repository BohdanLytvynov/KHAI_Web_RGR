using AutoMapper;
using BookStore.BLL.Dto.Genre;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Mapping.Genres
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, CreateGenreDto>()
                .ForSourceMember(x => x.Book_Geners, conf => conf.DoNotValidate())
                .ReverseMap();
            CreateMap<Genre, GenreDto>()
                .ForSourceMember(x => x.Book_Geners, conf => conf.DoNotValidate())
                .ReverseMap();
        }

    }
}
