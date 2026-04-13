using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Mappings
{
    public class BorrowProfile : Profile
    {
        public BorrowProfile()
        {
            CreateMap<BorrowRequest, Borrow>()
            .ForMember(dest => dest.BorrowDate, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<Borrow, BorrowResponse>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Unknown"))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
