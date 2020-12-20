using AutoMapper;
using Movies.DTO;
using Movies.Models;

namespace Movies.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movie, MovieRead>();
            CreateMap<MovieWrite, Movie>();

            CreateMap<ActorWrite, Actor>();
            CreateMap<Actor, ActorRead>().ReverseMap();
            CreateMap<ActorInMovie, Actor>().ReverseMap();

            CreateMap<Movie, Movie>();

            //update
            CreateMap<ActorId, Actor>().ReverseMap();
            CreateMap<UpdateMovie, Movie>().ReverseMap();
        }
    }    
}