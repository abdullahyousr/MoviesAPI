using AutoMapper;
using Movies.BL.Models;
using Movies.DAL.Entity;

namespace Movies.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Genre,GenreVM>();
            CreateMap<GenreVM,Genre>();

            CreateMap<Movie, MovieVM>();
            CreateMap<MovieVM, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}