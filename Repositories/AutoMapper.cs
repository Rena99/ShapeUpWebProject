using AutoMapper;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Comon;

namespace Repositories
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Members, MembersDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.AccountDate, opt => opt.MapFrom(src => src.AccountDate))
                .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src => src.UserPassword))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<Point, PointDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Y));
            CreateMap<Projects, ProjectsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.ProjectDate, opt => opt.MapFrom(src => src.ProjectDate))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus));
            CreateMap<Result, ResultsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PointOfShapeX, opt => opt.MapFrom(src => src.PointOfShapeX))
                .ForMember(dest => dest.PointOfShapeY, opt => opt.MapFrom(src => src.PointOfShapeY))
                .ForMember(dest => dest.PointOnAreaX, opt => opt.MapFrom(src => src.PointOnAreaX))
                .ForMember(dest => dest.PointOnAreaY, opt => opt.MapFrom(src => src.PointOnAreaY));
            CreateMap<Shapes, ShapesDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));
        }
    }
}
