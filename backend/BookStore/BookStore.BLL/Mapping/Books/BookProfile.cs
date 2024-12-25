using AutoMapper;
using BookStore.BLL.Dto.Book;
using BookStore.BLL.Dto.Genre;
using BookStore.BLL.ValueResolvers;
using BookStore.DAL.Entities;

namespace BookStore.BLL.Mapping.Books
{
    public class BookProfile : Profile
    {

        public BookProfile() 
        {
            CreateMap<Book, SimpleBookDto>()
                .ForMember(x => x.Id, conf => conf.MapFrom(x => x.Id))
                .ForMember(x => x.Name, conf => conf.MapFrom(x => x.Name))
                .ForMember(x => x.PubYear, conf => conf.MapFrom(x => x.PubYear))
                .ForMember(x => x.Geners, conf => conf
                .MapFrom(x => x.Book_Genres
                .Where(x => true).Select(x => new GenreDto() { Name = x.Genre.Name, Id = x.Genre.Id })));

            CreateMap<SimpleBookDto, Book>()
                .ForMember(x => x.Name, conf => conf.MapFrom(x => x.Name))
                .ForMember(x => x.PubYear, conf => conf.MapFrom(x => x.PubYear))
                .ForMember(x => x.Book_Authors, conf => conf.Ignore())
                .ForMember(x => x.Book_Genres, conf => conf.Ignore());
            
        }
    }
}
