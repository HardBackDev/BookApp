using AutoMapper;
using BookAppServer.Dto;
using BookAppServer.Dto.AuthorsDto;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Dto.CommentDto;
using BookAppServer.Dto.UserDto;
using BookAppServer.Models;

namespace BookAppServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreation, Book>();
            CreateMap<BookForUpdate, Book>();
            CreateMap<BookForAuthorCreation, Book>();
            CreateMap<BookForAuthorCreation, BookForCreation>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorForCreation, Author>();
            CreateMap<AuthorForUpdate, Author>();

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Comment, CommentDto>()
                .ForMember("UserName", conf =>
                conf.MapFrom(c => c.User.UserName));
            CreateMap<CommentForCreation, Comment>();
            CreateMap<CommentForUpdate, Comment>();
        }
    }
}
