using AutoMapper;
using BookshopApi.DTO;
using BookshopApi.DTO.Book;
using BookshopApi.Models;

namespace BookshopApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Book, BookDTO>().ReverseMap();
/*            CreateMap<Book, BookDTO>()
                .ForMember(m => m.PublisherId, p => p.MapFrom(a => a.Publisher.Id))
                .ForMember(m => m.PublisherName, p => p.MapFrom(a => a.Publisher.PublisherName))
                .ReverseMap();*/
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();

            CreateMap<CreateBookDTO, Book>()
                .ForMember(b => b.Publisher,
                    p => p.MapFrom(dto => new Publisher()
                    { PublisherName = dto.PublisherName }));
/*                .ForMember(b => b.BookAuthors, 
                    a => a.MapFrom(dto =>  new Author()
                        { FirstName = dto.AuthorFristName, MiddleName = dto.AuthorMiddleName, LastName = dto.AuthorLastName }))
                .ReverseMap();*/
        }
    }
}
