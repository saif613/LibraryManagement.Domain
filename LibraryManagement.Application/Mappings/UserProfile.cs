using AutoMapper;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
    .ForMember(dest => dest.BorrowHistory, opt => opt.MapFrom(src => src.Borrows)) 
    .ForMember(dest => dest.TotalBorrowedBooks, opt => opt.MapFrom(src => src.Borrows.Count))
    .ForMember(dest => dest.CurrentActiveBorrows, opt => opt.MapFrom(src => src.Borrows.Count(b => b.ReturnDate == null)));

            CreateMap<Borrow, UserBorrowDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book!.Title)) 
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.ReturnDate != null ? "Returned" :
                    src.DueDate < DateTime.Now ? "Overdue" : "Active"));
        }
    }
}
