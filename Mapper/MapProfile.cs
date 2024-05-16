using AutoMapper;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Mapper;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Album, AlbumDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
            .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks));

        CreateMap<Artist, ArtistDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums));

        CreateMap<Genre, GenreDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Playlist, PlaylistDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks));

        CreateMap<Track, TrackDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Artist));
    }
}