using AutoMapper;
using BookshopApi.DTO;
using BookshopApi.Models;

namespace BookshopApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            //CreateMap<Author, AuthorDTO>().ReverseMap();
        }
    }
}
